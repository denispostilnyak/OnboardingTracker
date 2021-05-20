using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.SeniorityLevels.Models;
using OnBoardingTracker.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.SeniorityLevels.Queries
{
    public class GetSeniorityLevels : IRequest<SeniorityLevelList>
    {
        public class Handler : IRequestHandler<GetSeniorityLevels, SeniorityLevelList>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper)
            {
                this.mapper = mapper;
                this.dbContext = dbContext;
            }

            public async Task<SeniorityLevelList> Handle(GetSeniorityLevels request, CancellationToken cancellationToken)
            {
                var seniorityLevels = await dbContext.SeniorityLevels.
                    AsNoTracking().
                    Select(x => mapper.Map<SeniorityLevelModel>(x)).
                    ToListAsync(cancellationToken);
                return new SeniorityLevelList { Items = seniorityLevels };
            }
        }
    }
}
