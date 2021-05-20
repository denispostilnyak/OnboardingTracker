namespace OnBoardingTracker.Domain.Entities
{
    public class CandidateVacancy
    {
        public int Id { get; set; }

        public int CandidateId { get; set; }

        public int VacancyId { get; set; }

        public virtual Candidate Candidate { get; set; }

        public virtual Vacancy Vacancy { get; set; }
    }
}
