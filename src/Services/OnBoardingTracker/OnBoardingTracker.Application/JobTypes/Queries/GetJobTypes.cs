using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.JobTypes.Models;
using OnBoardingTracker.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.JobTypes.Queries
{
    public class GetJobTypes : IRequest<JobTypeList>
    {
        public class Handler : IRequestHandler<GetJobTypes, JobTypeList>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext context, IMapper mapper)
            {
                dbContext = context;
                this.mapper = mapper;
            }

            public async Task<JobTypeList> Handle(GetJobTypes request, CancellationToken cancellationToken)
            {
                var resultTypes = await dbContext.JobTypes
                                    .AsNoTracking()
                                    .Select(jobType => mapper.Map<JobTypeModel>(jobType))
                                    .ToListAsync(cancellationToken);

                return new JobTypeList { Items = resultTypes };
            }
        }
    }
}
