using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.JobTypes.Models;
using OnBoardingTracker.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.JobTypes.Commands
{
    public class CreateJobType : IRequest<JobTypeModel>
    {
        public string Name { get; set; }

        public class Validator : AbstractValidator<CreateJobType>
        {
            public Validator()
            {
                RuleFor(jobType => jobType.Name).NotNull().NotEmpty();
            }
        }

        public class Handler : IRequestHandler<CreateJobType, JobTypeModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext context, IMapper mapper)
            {
                dbContext = context;
                this.mapper = mapper;
            }

            public async Task<JobTypeModel> Handle(CreateJobType request, CancellationToken cancellationToken)
            {
                var jobType = mapper.Map<Domain.Entities.JobType>(request);

                dbContext.JobTypes.Add(jobType);
                await dbContext.SaveChangesAsync(cancellationToken);

                return mapper.Map<JobTypeModel>(jobType);
            }
        }
    }
}
