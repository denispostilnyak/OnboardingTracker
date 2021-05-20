using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Skills.Models;
using OnBoardingTracker.Domain.Entities;
using OnBoardingTracker.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = OnBoardingTracker.Application.Infrastructure.Exceptions.ValidationException;

namespace OnBoardingTracker.Application.Candidates.Commands
{
    public class AssignCandidateSkills : IRequest<SkillList>
    {
        public int CandidateId { get; set; }

        public IEnumerable<int> SkillIds { get; set; }

        public class Validator : AbstractValidator<AssignCandidateSkills>
        {
            public Validator()
            {
                RuleFor(x => x.SkillIds).NotEmpty();
                RuleFor(x => x.CandidateId).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<AssignCandidateSkills, SkillList>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<SkillList> Handle(AssignCandidateSkills request, CancellationToken cancellationToken)
            {
                var skillModels = new List<SkillModel>();
                var skills = await dbContext.Skills.ToListAsync(cancellationToken);
                var candidateSkills = await dbContext.CandidateSkills.Where(x => x.CandidateId == request.CandidateId).ToListAsync(cancellationToken);
                if (!candidateSkills.Any())
                {
                    throw new ValidationException($"Candidate with an id of {request.CandidateId} was not found");
                }

                if (request.SkillIds.Any(skillId => !skills.Exists(x => x.Id == skillId)))
                {
                    throw new ValidationException("Skill does not exist");
                }

                foreach (var skillId in request.SkillIds)
                {
                    if (candidateSkills.Exists(x => x.SkillId == skillId))
                    {
                        throw new ValidationException($"Candidate with an id of {request.CandidateId} already has" +
                            $" the skill with an id of {skillId} attached");
                    }

                    dbContext.CandidateSkills.Add(new CandidateSkill
                    {
                        CandidateId = request.CandidateId,
                        SkillId = skillId
                    });
                    skillModels.Add(mapper.Map<SkillModel>(skills.First(x => x.Id == skillId)));
                }

                await dbContext.SaveChangesAsync(cancellationToken);
                return new SkillList { Items = skillModels };
            }
        }
    }
}
