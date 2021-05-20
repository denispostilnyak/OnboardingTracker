using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Vacancy.Models;
using OnBoardingTracker.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = OnBoardingTracker.Application.Infrastructure.Exceptions.ValidationException;

namespace OnBoardingTracker.Application.Vacancy.Comands
{
    public class DeleteVacancy : IRequest<VacancyModel>
    {
        public int Id { get; set; }

        public class Validator : AbstractValidator<DeleteVacancy>
        {
            public Validator()
            {
                RuleFor(s => s.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<DeleteVacancy, VacancyModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<VacancyModel> Handle(DeleteVacancy request, CancellationToken cancellationToken)
            {
                var vacancy = await dbContext.Vacancies
                    .FindAsync(new object[] { request.Id }, cancellationToken);

                if (vacancy == null)
                {
                    throw new NotFoundException(typeof(Domain.Entities.Vacancy).Name);
                }

                var isExistOnInterview = dbContext.Interviews.Any(interview => interview.VacancyId == request.Id);

                if (isExistOnInterview)
                {
                    throw new ValidationException($"Vacancy with id: {request.Id} has relation with Interviews. Firstly, try to remove interviews.");
                }

                dbContext.Vacancies.Remove(vacancy);
                await dbContext.SaveChangesAsync(cancellationToken);

                return mapper.Map<VacancyModel>(vacancy);
            }
        }
    }
}
