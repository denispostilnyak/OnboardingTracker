using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.Skills.Models;
using OnBoardingTracker.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Skills.Commands
{
    public class CreateSkill : IRequest<SkillModel>
    {
        public string Name { get; set; }

        public class Validator : AbstractValidator<CreateSkill>
        {
            public Validator()
            {
                RuleFor(skill => skill.Name).NotNull().NotEmpty();
            }
        }

        public class Handler : IRequestHandler<CreateSkill, SkillModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext context, IMapper mapper)
            {
                dbContext = context;
                this.mapper = mapper;
            }

            public async Task<SkillModel> Handle(CreateSkill request, CancellationToken cancellationToken)
            {
                var skill = mapper.Map<Domain.Entities.Skill>(request);

                dbContext.Skills.Add(skill);

                await dbContext.SaveChangesAsync(cancellationToken);

                return mapper.Map<SkillModel>(skill);
            }
        }
    }
}
