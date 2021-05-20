using System.Collections.Generic;

namespace OnBoardingTracker.Application.Infrastructure.Models
{
    public class ItemsCollection<T>
    {
        public IEnumerable<T> Items { get; set; }
    }
}
