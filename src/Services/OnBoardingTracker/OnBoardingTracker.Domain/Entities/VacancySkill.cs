namespace OnBoardingTracker.Domain.Entities
{
    public class VacancySkill
    {
        public int Id { get; set; }

        public int VacancyId { get; set; }

        public int SkillId { get; set; }

        public virtual Skill Skill { get; set; }

        public virtual Vacancy Vacancy { get; set; }
    }
}
