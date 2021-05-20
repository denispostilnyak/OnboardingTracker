using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.Skills.Models;
using OnBoardingTracker.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Skills.Queries
{
    public class GetSkillById : IRequest<SkillModel>
    {
        public int Id { get; set; }

        public class Validator : AbstractValidator<GetSkillById>
        {
            public Validator()
            {
                RuleFor(skill => skill.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<GetSkillById, SkillModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext context, IMapper mapper)
            {
                dbContext = context;
                this.mapper = mapper;
            }

            public async Task<SkillModel> Handle(GetSkillById request, CancellationToken cancellationToken)
            {
                var skill = await dbContext.Skills
                    .FindAsync(new object[] { request.Id }, cancellationToken);

                if (skill == null)
                {
                    return null;
                }

                return mapper.Map<SkillModel>(skill);
            }
        }
    }
}
