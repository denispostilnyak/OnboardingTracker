using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Interviews.Models;
using OnBoardingTracker.Domain.Entities;
using OnBoardingTracker.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Interviews.Commands
{
    public class UpdateInterview : IRequest<InterviewModel>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int CandidateId { get; set; }

        public int RecruiterId { get; set; }

        public int VacancyId { get; set; }

        public DateTime StartingTime { get; set; }

        public DateTime EndingTime { get; set; }

        public class Validator : AbstractValidator<UpdateInterview>
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

        public class Handler : IRequestHandler<UpdateInterview, InterviewModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<InterviewModel> Handle(UpdateInterview request, CancellationToken cancellationToken)
            {
                var interview = await dbContext.Interviews.FindAsync(new object[] { request.Id }, cancellationToken);
                if (interview == null)
                {
                    throw new NotFoundException(typeof(Interview).Name);
                }

                interview.Title = request.Title;
                interview.CandidateId = request.CandidateId;
                interview.VacancyId = request.VacancyId;
                interview.RecruiterId = request.RecruiterId;
                interview.StartingTime = request.StartingTime;
                interview.EndingTime = request.EndingTime;

                await dbContext.SaveChangesAsync(cancellationToken);
                return mapper.Map<InterviewModel>(interview);
            }
        }
    }
}
