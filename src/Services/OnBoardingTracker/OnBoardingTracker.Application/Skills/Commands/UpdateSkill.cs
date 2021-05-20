using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Skills.Models;
using OnBoardingTracker.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Skills.Commands
{
    public class UpdateSkill : IRequest<SkillModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public class Validator : AbstractValidator<UpdateSkill>
        {
            public Validator()
            {
                RuleFor(item => item.Name).NotEmpty();
                RuleFor(item => item.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<UpdateSkill, SkillModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext context, IMapper mapper)
            {
                dbContext = context;
                this.mapper = mapper;
            }

            public async Task<SkillModel> Handle(UpdateSkill request, CancellationToken cancellationToken)
            {
                var skill = await dbContext.Skills
                    .FindAsync(new object[] { request.Id }, cancellationToken);

                if (skill == null)
                {
                    throw new NotFoundException(typeof(Domain.Entities.Skill).Name);
                }

                skill.Name = request.Name;
                await dbContext.SaveChangesAsync(cancellationToken);

                return mapper.Map<SkillModel>(skill);
            }
        }
    }
}
