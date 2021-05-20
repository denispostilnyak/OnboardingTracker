using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.Interviews.Models;
using OnBoardingTracker.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Interviews.Queries
{
    public class GetInterviewById : IRequest<InterviewModel>
    {
        public int Id { get; set; }

        public class Validator : AbstractValidator<GetInterviewById>
        {
            public Validator()
            {
                RuleFor(x => x.Id).GreaterThan(0);
            }
        }

        public class Handler : IRequestHandler<GetInterviewById, InterviewModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<InterviewModel> Handle(GetInterviewById request, CancellationToken cancellationToken)
            {
                var interview = await dbContext.Interviews.FindAsync(new object[] { request.Id }, cancellationToken);

                if (interview == null)
                {
                    return null;
                }

                return mapper.Map<InterviewModel>(interview);
            }
        }
    }
}
