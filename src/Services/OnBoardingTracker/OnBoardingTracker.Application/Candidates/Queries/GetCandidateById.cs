using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.Candidates.Models;
using OnBoardingTracker.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Candidates.Queries
{
    public class GetCandidateById : IRequest<CandidateModel>
    {
        public int Id { get; set; }

        public class Validator : AbstractValidator<GetCandidateById>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<GetCandidateById, CandidateModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<CandidateModel> Handle(GetCandidateById request, CancellationToken cancellationToken)
            {
                var candidate = await dbContext.Candidates.FindAsync(new object[] { request.Id }, cancellationToken);

                if (candidate == null)
                {
                    return null;
                }

                return mapper.Map<CandidateModel>(candidate);
            }
        }
    }
}
