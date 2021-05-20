using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.JobTypes.Models;
using OnBoardingTracker.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.JobTypes.Commands
{
    public class UpdateJobType : IRequest<JobTypeModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public class Validator : AbstractValidator<UpdateJobType>
        {
            public Validator()
            {
                RuleFor(item => item.Name).NotEmpty();
                RuleFor(item => item.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<UpdateJobType, JobTypeModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext context, IMapper mapper)
            {
                dbContext = context;
                this.mapper = mapper;
            }

            public async Task<JobTypeModel> Handle(UpdateJobType request, CancellationToken cancellationToken)
            {
                var jobType = await dbContext.JobTypes.FindAsync(new object[] { request.Id }, cancellationToken);
                if (jobType == null)
                {
                    throw new NotFoundException(typeof(Domain.Entities.JobType).Name);
                }

                jobType.Name = request.Name;
                await dbContext.SaveChangesAsync(cancellationToken);

                return mapper.Map<JobTypeModel>(jobType);
            }
        }
    }
}
