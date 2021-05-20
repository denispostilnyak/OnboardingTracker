using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.JobTypes.Models;
using OnBoardingTracker.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.JobTypes.Queries
{
    public class GetJobTypeById : IRequest<JobTypeModel>
    {
        public int Id { get; set; }

        public class Validator : AbstractValidator<GetJobTypeById>
        {
            public Validator()
            {
                RuleFor(jobType => jobType.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<GetJobTypeById, JobTypeModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext context, IMapper mapper)
            {
                dbContext = context;
                this.mapper = mapper;
            }

            public async Task<JobTypeModel> Handle(GetJobTypeById request, CancellationToken cancellationToken)
            {
                var jobType = await dbContext.JobTypes
                    .FindAsync(new object[] { request.Id }, cancellationToken);
                if (jobType == null)
                {
                    return null;
                }

                return mapper.Map<JobTypeModel>(jobType);
            }
        }
    }
}
