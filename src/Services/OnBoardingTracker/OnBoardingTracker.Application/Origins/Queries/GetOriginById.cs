using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.Origins.Models;
using OnBoardingTracker.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Origins.Queries
{
    public class GetOriginById : IRequest<OriginModel>
    {
        public int Id { get; set; }

        public class Validator : AbstractValidator<GetOriginById>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<GetOriginById, OriginModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<OriginModel> Handle(GetOriginById request, CancellationToken cancellationToken)
            {
                var candidateOrigin = await dbContext.CandidateOrigins.FindAsync(new object[] { request.Id }, cancellationToken);
                if (candidateOrigin == null)
                {
                    return null;
                }

                return mapper.Map<OriginModel>(candidateOrigin);
            }
        }
    }
}
