using System.Collections.Generic;

namespace OnBoardingTracker.Domain.Entities
{
    public class VacancyStatus : EntityBase
    {
        public VacancyStatus()
        {
            Vacancies = new HashSet<Vacancy>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Vacancy> Vacancies { get; set; }
    }
}
