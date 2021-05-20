using System;

namespace OnBoardingTracker.Application.Interviews.Models
{
    public class InterviewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int CandidateId { get; set; }

        public int RecruiterId { get; set; }

        public int VacancyId { get; set; }

        public DateTime StartingTime { get; set; }

        public DateTime EndingTime { get; set; }
    }
}
