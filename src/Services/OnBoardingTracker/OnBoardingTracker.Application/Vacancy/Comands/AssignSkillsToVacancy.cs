using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Skills.Models;
using OnBoardingTracker.Domain.Entities;
using OnBoardingTracker.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = OnBoardingTracker.Application.Infrastructure.Exceptions.ValidationException;

namespace OnBoardingTracker.Application.Vacancy.Comands
{
    public class AssignSkillsToVacancy
        : IRequest<SkillList>
    {
        public int VacancyId { get; set; }

        public IEnumerable<int> SkillIdList { get; set; }

        public class Validator
            : AbstractValidator<AssignSkillsToVacancy>
        {
            public Validator()
            {
                RuleFor(item => item.VacancyId).NotEmpty();
                RuleFor(item => item.SkillIdList).NotEmpty();
            }
        }

        public class Handler
            : IRequestHandler<AssignSkillsToVacancy, SkillList>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext context, IMapper mapper)
            {
                dbContext = context;
                this.mapper = mapper;
            }

            public async Task<SkillList> Handle(AssignSkillsToVacancy request, CancellationToken cancellationToken)
            {
                var skillIdList = request.SkillIdList;
                var vacancyId = request.VacancyId;
                var skillList = new List<SkillModel>();

                var vacancy = await dbContext.Vacancies
                    .FindAsync(new object[] { vacancyId }, cancellationToken);
                if (vacancy == null)
                {
                    throw new NotFoundException(typeof(Domain.Entities.Vacancy).Name);
                }

                var skills = await dbContext.Skills.ToListAsync(cancellationToken);
                var vacancySkills = await dbContext.VacancySkills
                    .Where(x => x.VacancyId == request.VacancyId)
                    .ToListAsync(cancellationToken);

                foreach (var skillId in skillIdList)
                {
                    if (!skills.Exists(x => x.Id == skillId))
                    {
                        throw new NotFoundException($"skill with id: {skillId}");
                    }

                    if (vacancySkills.Exists(x => x.SkillId == skillId))
                    {
                        throw new ValidationException($"Skill with id: {skillId} is already exist on this vacancy");
                    }

                    dbContext.VacancySkills.Add(
                       new VacancySkill
                       {
                           SkillId = skillId,
                           VacancyId = vacancyId
                       });

                    skillList.Add(mapper.Map<SkillModel>(skills.FirstOrDefault(x => x.Id == skillId)));
                }

                await dbContext.SaveChangesAsync();
                return new SkillList { Items = skillList };
            }
        }
    }
}
