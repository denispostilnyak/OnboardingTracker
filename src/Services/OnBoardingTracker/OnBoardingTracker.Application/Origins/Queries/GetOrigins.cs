using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Origins.Models;
using OnBoardingTracker.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Origins.Queries
{
    public class GetOrigins : IRequest<OriginList>
    {
        public class Handler : IRequestHandler<GetOrigins, OriginList>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper)
            {
                this.mapper = mapper;
                this.dbContext = dbContext;
            }

            public async Task<OriginList> Handle(GetOrigins request, CancellationToken cancellationToken)
            {
                var originsList = await dbContext.CandidateOrigins.
                    AsNoTracking().
                    Select(x => mapper.Map<OriginModel>(x)).
                    ToListAsync(cancellationToken);
                return new OriginList { Items = originsList };
            }
        }
    }
}
