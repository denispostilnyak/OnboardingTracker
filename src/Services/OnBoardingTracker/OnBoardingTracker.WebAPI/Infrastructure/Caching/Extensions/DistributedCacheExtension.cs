using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.WebApi.Infrastructure.Caching.Extensions
{
    public static class DistributedCacheExtension
    {
        public static async Task SetAsync<T>(
            this IDistributedCache distributedCache, string key, T value, DistributedCacheEntryOptions options, CancellationToken token = default)
        {
            if (distributedCache == null)
            {
                throw new ArgumentNullException(nameof(distributedCache).ToString());
            }

            await distributedCache.SetAsync(
                key, JsonSerializer.SerializeToUtf8Bytes(value, new JsonSerializerOptions { IgnoreNullValues = true }), options, token);
        }

        public static async Task<T> GetAsync<T>(
            this IDistributedCache distributedCache, string key, CancellationToken token = default)
            where T : class
        {
            if (distributedCache == null)
            {
                throw new ArgumentNullException(nameof(distributedCache).ToString());
            }

            var result = await distributedCache.GetAsync(key, token);
            return result == null ? null : JsonSerializer.Deserialize<T>(result);
        }
    }
}
