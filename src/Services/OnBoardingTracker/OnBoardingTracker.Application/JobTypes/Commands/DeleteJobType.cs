using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.JobTypes.Models;
using OnBoardingTracker.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = OnBoardingTracker.Application.Infrastructure.Exceptions.ValidationException;

namespace OnBoardingTracker.Application.JobTypes.Commands
{
    public class DeleteJobType : IRequest<JobTypeModel>
    {
        public int Id { get; set; }

        public class Validator : AbstractValidator<DeleteJobType>
        {
            public Validator()
            {
                RuleFor(jobType => jobType.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<DeleteJobType, JobTypeModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext context, IMapper mapper)
            {
                dbContext = context;
                this.mapper = mapper;
            }

            public async Task<JobTypeModel> Handle(DeleteJobType request, CancellationToken cancellationToken)
            {
                var jobType = await dbContext.JobTypes.FindAsync(new object[] { request.Id }, cancellationToken);
                if (jobType == null)
                {
                    throw new NotFoundException(typeof(Domain.Entities.JobType).Name);
                }

                if (dbContext.Vacancies.Any(x => x.JobTypeId == request.Id))
                {
                    throw new ValidationException($"Job Type with id: {request.Id} has relation with Vacancies.Delete related Vacancies first.");
                }

                dbContext.JobTypes.Remove(jobType);
                await dbContext.SaveChangesAsync(cancellationToken);

                return mapper.Map<JobTypeModel>(jobType);
            }
        }
    }
}
