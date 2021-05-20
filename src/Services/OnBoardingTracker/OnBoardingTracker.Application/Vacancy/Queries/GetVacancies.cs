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
    public class GetVacancies : IRequest<VacancyList>
    {
        public class Handler : IRequestHandler<GetVacancies, VacancyList>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext context, IMapper mapper)
            {
                dbContext = context;
                this.mapper = mapper;
            }

            public async Task<VacancyList> Handle(GetVacancies request, CancellationToken cancellationToken)
            {
                var allVacancies = await dbContext.Vacancies
                    .AsNoTracking()
                    .Select(vacancy => mapper.Map<VacancyModel>(vacancy))
                    .ToListAsync(cancellationToken);

                return new VacancyList { Items = allVacancies };
            }
        }
    }
}
