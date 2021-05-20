using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.VacancyStatuses.Models;
using OnBoardingTracker.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.VacancyStatuses.Queries
{
    public class GetVacancyStatuses : IRequest<VacancyStatusList>
    {
       public class Handler : IRequestHandler<GetVacancyStatuses, VacancyStatusList>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(IMapper mapper, OnboardingTrackerContext dbContext)
            {
                this.mapper = mapper;
                this.dbContext = dbContext;
            }

            public async Task<VacancyStatusList> Handle(GetVacancyStatuses request, CancellationToken cancellationToken)
            {
                var vacancyStatuses = await dbContext.VacancyStatuses.
                    AsNoTracking().
                    Select(x => mapper.Map<VacancyStatusModel>(x)).
                    ToListAsync(cancellationToken);

                return new VacancyStatusList { Items = vacancyStatuses };
            }
        }
    }
}
