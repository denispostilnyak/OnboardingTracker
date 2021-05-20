using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.Recruiters.Models;
using OnBoardingTracker.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Recruiters.Queries
{
    public class GetRecruiterById : IRequest<RecruiterModel>
    {
        public int Id { get; set; }

        public class Validator : AbstractValidator<GetRecruiterById>
        {
            public Validator()
            {
                RuleFor(recruiter => recruiter.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<GetRecruiterById, RecruiterModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext context, IMapper mapper)
            {
                dbContext = context;
                this.mapper = mapper;
            }

            public async Task<RecruiterModel> Handle(GetRecruiterById request, CancellationToken cancellationToken)
            {
                var recruiter = await dbContext.Recruiters.
                    FindAsync(new object[] { request.Id }, cancellationToken);

                if (recruiter == null)
                {
                    return null;
                }

                return mapper.Map<RecruiterModel>(recruiter);
            }
        }
    }
}
