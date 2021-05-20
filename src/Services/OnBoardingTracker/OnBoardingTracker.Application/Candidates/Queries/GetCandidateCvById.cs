using FluentValidation;
using MediatR;
using OnBoardingTracker.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Candidates.Queries
{
    public class GetCandidateCvById : IRequest<Uri>
    {
        public int Id { get; set; }

        public class Validator : AbstractValidator<GetCandidateCvById>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<GetCandidateCvById, Uri>
        {
            private readonly OnboardingTrackerContext dbContext;

            public Handler(OnboardingTrackerContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<Uri> Handle(GetCandidateCvById request, CancellationToken cancellationToken)
            {
                var candidate = await dbContext.Candidates.FindAsync(new object[] { request.Id }, cancellationToken);
                return candidate.CvUrl;
            }
        }
    }
}
