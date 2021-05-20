using System;
using System.Collections.Generic;

namespace OnBoardingTracker.Domain.Entities
{
    public class Recruiter : EntityBase
    {
        public Recruiter()
        {
            Vacancies = new HashSet<Vacancy>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public Uri PictureUrl { get; set; }

        public virtual ICollection<Vacancy> Vacancies { get; set; }

        public virtual ICollection<Interview> Interviews { get; set; }
    }
}
