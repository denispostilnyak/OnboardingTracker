using System;

namespace OnBoardingTracker.Domain.Entities
{
    public class EntityBase
    {
        public int CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public bool IsDeleted { get; set; }
    }
}
