using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using UrlShortener.Application.Interfaces;

namespace UrlShortener.Application.Services;
public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    public CacheService(IDistributedCache cache)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> func, TimeSpan? absoluteExpiration = null)
    {
        var cached = await _cache.GetAsync(key);

        string serializedCache;

        if (cached != null)
        {
            serializedCache = Encoding.UTF8.GetString(cached);

            return JsonSerializer.Deserialize<T>(serializedCache);
        }

        var result = await func();

        serializedCache = JsonSerializer.Serialize(result);

        byte[] byteCache = Encoding.UTF8.GetBytes(serializedCache);

        await _cache.SetAsync(key, byteCache, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = absoluteExpiration ?? TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromMinutes(2),
        });

        return result;
    }
}