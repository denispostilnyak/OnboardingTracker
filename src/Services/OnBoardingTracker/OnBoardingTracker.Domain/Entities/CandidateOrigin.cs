using System.Collections.Generic;

namespace OnBoardingTracker.Domain.Entities
{
    public class CandidateOrigin : EntityBase
    {
        public CandidateOrigin()
        {
            Candidates = new HashSet<Candidate>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }
    }
}
