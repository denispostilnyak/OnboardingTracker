using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Recruiters.Models;
using OnBoardingTracker.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Recruiters.Queries
{
    public class GetRecruiters : IRequest<RecruiterList>
    {
        public class Handler : IRequestHandler<GetRecruiters, RecruiterList>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext context, IMapper mapper)
            {
                dbContext = context;
                this.mapper = mapper;
            }

            public async Task<RecruiterList> Handle(GetRecruiters request, CancellationToken cancellationToken)
            {
                var resultRecruiters = await dbContext.Recruiters
                    .AsNoTracking()
                    .Select(recruiter => mapper.Map<RecruiterModel>(recruiter))
                    .ToListAsync(cancellationToken);

                return new RecruiterList { Items = resultRecruiters };
            }
        }
    }
}
