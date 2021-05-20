using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.Vacancy.Models;
using OnBoardingTracker.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Vacancy.Queries
{
    public class GetVacancyById : IRequest<VacancyModel>
    {
        public int Id { get; set; }

        public class Validator : AbstractValidator<GetVacancyById>
        {
            public Validator()
            {
                RuleFor(s => s.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<GetVacancyById, VacancyModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext context, IMapper mapper)
            {
                dbContext = context;
                this.mapper = mapper;
            }

            public async Task<VacancyModel> Handle(GetVacancyById request, CancellationToken cancellationToken)
            {
                var vacancy = await dbContext.Vacancies
                    .FindAsync(new object[] { request.Id }, cancellationToken);

                if (vacancy == null)
                {
                    return null;
                }

                return mapper.Map<VacancyModel>(vacancy);
            }
        }
    }
}
