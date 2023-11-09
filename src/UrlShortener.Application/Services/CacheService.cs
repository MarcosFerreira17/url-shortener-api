using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using UrlShortener.Application.Interfaces;

namespace UrlShortener.Application.Services;
public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> func, TimeSpan? absoluteExpiration = null)
    {
        var cached = _cache.GetString(key);

        if (cached != null)
            return Task.FromResult(JsonSerializer.Deserialize<T>(cached));

        var result = func();

        _cache.SetString(key, JsonSerializer.Serialize(result), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = absoluteExpiration is null ? TimeSpan.FromMinutes(5) : absoluteExpiration
        });

        return result;
    }
}