using System;

namespace OnBoardingTracker.Domain.Entities
{
    public class Interview : EntityBase
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int CandidateId { get; set; }

        public int VacancyId { get; set; }

        public int RecruiterId { get; set; }

        public DateTime StartingTime { get; set; }

        public DateTime EndingTime { get; set; }

        public virtual Candidate Candidate { get; set; }

        public virtual Vacancy Vacancy { get; set; }

        public virtual Recruiter Recruiter { get; set; }
    }
}
