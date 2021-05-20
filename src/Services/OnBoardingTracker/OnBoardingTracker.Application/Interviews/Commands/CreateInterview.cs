using AutoMapper;
using AutoMapper.Configuration;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Interviews.Models;
using OnBoardingTracker.Domain.Entities;
using OnBoardingTracker.Infrastructure.EmailService.Abstract;
using OnBoardingTracker.Persistence;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace OnBoardingTracker.Application.Interviews.Commands
{
    public class CreateInterview : IRequest<InterviewModel>
    {
        public string Title { get; set; }

        public int CandidateId { get; set; }

        public int VacancyId { get; set; }

        public int RecruiterId { get; set; }

        public DateTime StartingTime { get; set; }

        public DateTime EndingTime { get; set; }

        public class Validator : AbstractValidator<CreateInterview>
        {
            public Validator()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.VacancyId).GreaterThan(0);
                RuleFor(x => x.CandidateId).GreaterThan(0);
                RuleFor(x => x.RecruiterId).GreaterThan(0);
                RuleFor(x => x.EndingTime).GreaterThan(x => x.StartingTime);
            }
        }

        public class Handler : IRequestHandler<CreateInterview, InterviewModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;
            private readonly IEmailService emailService;
            private readonly IConfiguration configuration;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper, IEmailService emailService, IConfiguration configuration)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
                this.emailService = emailService;
                this.configuration = configuration;
            }

            public async Task<InterviewModel> Handle(CreateInterview request, CancellationToken cancellationToken)
            {
                var candidate = await dbContext.Candidates.FindAsync(new object[] { request.CandidateId }, cancellationToken);
                if (candidate == null)
                {
                    throw new NotFoundException(typeof(Candidate).Name);
                }

                var recruiter = await dbContext.Recruiters.FindAsync(new object[] { request.RecruiterId }, cancellationToken);
                if (recruiter == null)
                {
                    throw new NotFoundException(typeof(Recruiter).Name);
                }

                var toEmail = new List<string> { candidate.Email, recruiter.Email };
                var subject = configuration["Email:Interview:Subject"];
                var body = configuration["Email:Interview:Body"];

                await emailService.Send(toEmail, subject, body);

                var interview = mapper.Map<Interview>(request);
                dbContext.Interviews.Add(interview);
                await dbContext.SaveChangesAsync(cancellationToken);
                return mapper.Map<InterviewModel>(interview);
            }
        }
    }
}
