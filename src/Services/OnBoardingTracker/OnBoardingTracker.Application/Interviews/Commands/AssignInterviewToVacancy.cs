using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Interviews.Models;
using OnBoardingTracker.Domain.Entities;
using OnBoardingTracker.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Interviews.Commands
{
    public class AssignInterviewToVacancy : IRequest<InterviewModel>
    {
        public int InterviewId { get; set; }

        public int VacancyId { get; set; }

        public class Validator : AbstractValidator<AssignInterviewToVacancy>
        {
            public Validator()
            {
                RuleFor(x => x.InterviewId).NotEmpty();
                RuleFor(x => x.VacancyId).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<AssignInterviewToVacancy, InterviewModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<InterviewModel> Handle(AssignInterviewToVacancy request, CancellationToken cancellationToken)
            {
                var interview = await dbContext.Interviews.FindAsync(new object[] { request.InterviewId }, cancellationToken);
                if (interview == null)
                {
                    throw new NotFoundException(typeof(Interview).Name);
                }

                var vacancy = await dbContext.Vacancies.FindAsync(new object[] { request.VacancyId }, cancellationToken);

                if (vacancy == null)
                {
                    throw new NotFoundException(typeof(Interview).Name);
                }

                interview.VacancyId = request.VacancyId;
                await dbContext.SaveChangesAsync(cancellationToken);
                return mapper.Map<InterviewModel>(interview);
            }
        }
    }
}
