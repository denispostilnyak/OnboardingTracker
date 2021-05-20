using System;
using System.Collections.Generic;

namespace OnBoardingTracker.Domain.Entities
{
    public class Candidate : EntityBase
    {
        public Candidate()
        {
            CandidateSkills = new HashSet<CandidateSkill>();
            CandidateVacancies = new HashSet<CandidateVacancy>();
            Interviews = new HashSet<Interview>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public int OriginId { get; set; }

        public double YearsOfExperience { get; set; }

        public string CurrentJobInformation { get; set; }

        public virtual CandidateOrigin Origin { get; set; }

        public Uri CvUrl { get; set; }

        public Uri ProfilePicture { get; set; }

        public virtual ICollection<CandidateSkill> CandidateSkills { get; set; }

        public virtual ICollection<CandidateVacancy> CandidateVacancies { get; set; }

        public virtual ICollection<Interview> Interviews { get; set; }
    }
}
