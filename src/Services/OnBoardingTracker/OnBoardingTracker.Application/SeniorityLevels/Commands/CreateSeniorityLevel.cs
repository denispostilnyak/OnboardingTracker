using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.SeniorityLevels.Models;
using OnBoardingTracker.Domain.Entities;
using OnBoardingTracker.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.SeniorityLevels.Commands
{
    public class CreateSeniorityLevel : IRequest<SeniorityLevelModel>
    {
        public string Name { get; set; }

        public class Validator : AbstractValidator<CreateSeniorityLevel>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<CreateSeniorityLevel, SeniorityLevelModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<SeniorityLevelModel> Handle(CreateSeniorityLevel request, CancellationToken cancellationToken)
            {
                var seniorityLevel = mapper.Map<SeniorityLevel>(request);

                dbContext.SeniorityLevels.Add(seniorityLevel);
                await dbContext.SaveChangesAsync(cancellationToken);

                return mapper.Map<SeniorityLevelModel>(seniorityLevel);
            }
        }
    }
}
