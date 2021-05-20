using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Vacancy.Models;
using OnBoardingTracker.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Vacancy.Queries
{
    public class GetTopVacancies : IRequest<VacancyList>
    {
        public class Handler : IRequestHandler<GetTopVacancies, VacancyList>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext context, IMapper mapper)
            {
                dbContext = context;
                this.mapper = mapper;
            }

            public async Task<VacancyList> Handle(GetTopVacancies request, CancellationToken cancellationToken)
            {
                var topVacancies = await dbContext.Vacancies
                    .AsNoTracking()
                    .OrderByDescending(vacancy => vacancy.MaxSalary)
                    .Take(3)
                    .Select(vacancy => mapper.Map<VacancyModel>(vacancy))
                    .ToListAsync(cancellationToken);

                return new VacancyList { Items = topVacancies };
            }
        }
    }
}
