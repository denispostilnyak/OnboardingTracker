using System.Collections.Generic;

namespace OnBoardingTracker.Domain.Entities
{
    public class Skill : EntityBase
    {
        public Skill()
        {
            CandidateSkills = new HashSet<CandidateSkill>();
            VacancySkills = new HashSet<VacancySkill>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<CandidateSkill> CandidateSkills { get; set; }

        public virtual ICollection<VacancySkill> VacancySkills { get; set; }
    }
}
