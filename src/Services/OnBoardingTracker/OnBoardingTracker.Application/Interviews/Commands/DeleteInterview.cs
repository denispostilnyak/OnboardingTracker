using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Interviews.Models;
using OnBoardingTracker.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Interviews.Commands
{
    public class DeleteInterview : IRequest<InterviewModel>
    {
        public int Id { get; set; }

        public class Validator : AbstractValidator<DeleteInterview>
        {
            public Validator()
            {
                RuleFor(x => x.Id).GreaterThan(0);
            }
        }

        public class Handler : IRequestHandler<DeleteInterview, InterviewModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<InterviewModel> Handle(DeleteInterview request, CancellationToken cancellationToken)
            {
                var interview = await dbContext.Interviews.FindAsync(new object[] { request.Id }, cancellationToken);
                if (interview == null)
                {
                    throw new NotFoundException(typeof(Domain.Entities.Interview).Name);
                }

                dbContext.Interviews.Remove(interview);
                await dbContext.SaveChangesAsync(cancellationToken);
                return mapper.Map<InterviewModel>(interview);
            }
        }
    }
}
