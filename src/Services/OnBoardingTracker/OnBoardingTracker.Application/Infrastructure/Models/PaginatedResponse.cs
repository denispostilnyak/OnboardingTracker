namespace OnBoardingTracker.Application.Infrastructure.Models
{
    public class PaginatedResponse<T> : ItemsCollection<T>
    {
        public int TotalCount { get; set; }

        public int Count { get; set; }

        public int Page { get; set; }

        public int Limit { get; set; }
    }
}
