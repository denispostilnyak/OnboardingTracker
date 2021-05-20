using System.Linq;

namespace OnBoardingTracker.Application.Infrastructure.Paging.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> collection, int page, int limit)
        {
            var skip = (page - 1) * limit;
            var take = limit;
            return collection.Skip(skip).Take(take);
        }
    }
}
