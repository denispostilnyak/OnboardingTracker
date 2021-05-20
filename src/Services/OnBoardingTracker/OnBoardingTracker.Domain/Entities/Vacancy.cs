using System.Collections.Generic;

namespace OnBoardingTracker.Domain.Entities
{
    public class Vacancy : EntityBase
    {
        public Vacancy()
        {
            CandidateVacancies = new HashSet<CandidateVacancy>();
            Interviews = new HashSet<Interview>();
            VacancySkills = new HashSet<VacancySkill>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal MaxSalary { get; set; }

        public int AssignedRecruiterId { get; set; }

        public double WorkExperience { get; set; }

        public int SeniorityLevelId { get; set; }

        public int JobTypeId { get; set; }

        public int VacancyStatusId { get; set; }

        public virtual Recruiter AssignedRecruiter { get; set; }

        public virtual JobType JobType { get; set; }

        public virtual SeniorityLevel SeniorityLevel { get; set; }

        public virtual VacancyStatus VacancyStatus { get; set; }

        public virtual string VacancyPictureUrl { get; set; }

        public virtual ICollection<CandidateVacancy> CandidateVacancies { get; set; }

        public virtual ICollection<Interview> Interviews { get; set; }

        public virtual ICollection<VacancySkill> VacancySkills { get; set; }
    }
}
