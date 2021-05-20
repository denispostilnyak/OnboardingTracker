using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Candidates.Models;
using OnBoardingTracker.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Candidates.Queries
{
    public class GetCandidates : IRequest<CandidateList>
    {
        public class Handler : IRequestHandler<GetCandidates, CandidateList>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<CandidateList> Handle(GetCandidates request, CancellationToken cancellationToken)
            {
                var allCandidates = dbContext.Candidates.AsNoTracking();

                var candidates = await allCandidates.
                    Select(x => mapper.Map<CandidateModel>(x)).
                    ToListAsync(cancellationToken);

                var count = candidates.Count;
                var totalCount = allCandidates.Count();

                return new CandidateList { Items = candidates };
            }
        }
    }
}
