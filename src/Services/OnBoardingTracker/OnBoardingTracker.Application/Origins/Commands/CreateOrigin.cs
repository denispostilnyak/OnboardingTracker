using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.Origins.Models;
using OnBoardingTracker.Domain.Entities;
using OnBoardingTracker.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Origins.Commands
{
    public class CreateOrigin : IRequest<OriginModel>
    {
        public string Name { get; set; }

        public class Validator : AbstractValidator<CreateOrigin>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<CreateOrigin, OriginModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<OriginModel> Handle(CreateOrigin request, CancellationToken cancellationToken)
            {
                var candidateOrigin = mapper.Map<CandidateOrigin>(request);
                dbContext.CandidateOrigins.Add(candidateOrigin);
                await dbContext.SaveChangesAsync(cancellationToken);
                return mapper.Map<OriginModel>(candidateOrigin);
            }
        }
    }
}
