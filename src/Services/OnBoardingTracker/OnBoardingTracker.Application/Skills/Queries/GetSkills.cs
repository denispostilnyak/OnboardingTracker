using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Skills.Models;
using OnBoardingTracker.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Skills.Queries
{
    public class GetSkills : IRequest<SkillList>
    {
        public class Handler : IRequestHandler<GetSkills, SkillList>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;
            private OnboardingTrackerContext dbContext1;

            public Handler(OnboardingTrackerContext context, IMapper mapper)
            {
                dbContext = context;
                this.mapper = mapper;
            }

            public Handler(IMapper mapper, OnboardingTrackerContext dbContext1)
            {
                this.mapper = mapper;
                this.dbContext1 = dbContext1;
            }

            public async Task<SkillList> Handle(GetSkills request, CancellationToken cancellationToken)
            {
                var resultSkills = await dbContext.Skills
                                    .AsNoTracking()
                                    .Select(skill => mapper.Map<SkillModel>(skill))
                                    .ToListAsync(cancellationToken);

                return new SkillList { Items = resultSkills };
            }
        }
    }
}
