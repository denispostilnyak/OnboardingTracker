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
    public class DeleteSkill : IRequest<SkillModel>
    {
        public int Id { get; set; }

        public class Validator : AbstractValidator<DeleteSkill>
        {
            public Validator()
            {
                RuleFor(skill => skill.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<DeleteSkill, SkillModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext context, IMapper mapper)
            {
                dbContext = context;
                this.mapper = mapper;
            }

            public async Task<SkillModel> Handle(DeleteSkill request, CancellationToken cancellationToken)
            {
                var skill = await dbContext.Skills
                    .FindAsync(request.Id);
                if (skill == null)
                {
                    throw new NotFoundException(typeof(Domain.Entities.Skill).Name);
                }

                dbContext.Skills.Remove(skill);
                await dbContext.SaveChangesAsync(cancellationToken);

                return mapper.Map<SkillModel>(skill);
            }
        }
    }
}
