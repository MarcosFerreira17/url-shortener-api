using System.Runtime.Caching;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using UrlShortener.Application.Interfaces;

namespace UrlShortener.Application.Services;
public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    private readonly MemoryCache _inMemoryCache;
    private readonly ILogger<CacheService> _logger;
    public CacheService(IDistributedCache cache, ILogger<CacheService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _inMemoryCache = MemoryCache.Default;
    }

    /// <summary>
    /// Get or set in memory cache.
    /// </summary>
    /// <typeparam name="T">Accept a generic T class</typeparam>
    /// <param name="key">key for cache</param>
    /// <param name="func">Accepte a generic function</param>
    /// <param name="absoluteExpiration">Set time expiration for this key on cache</param>
    /// <returns>A generic T class</returns>
    public async Task<T> GetOrSetInMemory<T>(string key, Func<Task<T>> func, TimeSpan? absoluteExpiration = null)
    {
        T result;

        if (_inMemoryCache.Contains(key))
        {
            object cached = _inMemoryCache.Get(key);

            string serializedCache = JsonSerializer.Serialize(cached);

            result = JsonSerializer.Deserialize<T>(serializedCache);
        }
        else
        {
            result = await func.Invoke();

            if (result is null)
                return default;

            CacheItemPolicy cachePolicy = new()
            {
                AbsoluteExpiration = DateTimeOffset.Now.Add(absoluteExpiration ?? TimeSpan.FromMinutes(5))
            };

            _inMemoryCache.Add(key, result, cachePolicy);
        }

        return result;
    }

    /// <summary>
    /// Get or set a value in the distributed redis cache.
    /// </summary>
    /// <typeparam name="T">Accept a generic T class</typeparam>
    /// <param name="key">key for cache</param>
    /// <param name="func">Accepte a generic function</param>
    /// <param name="absoluteExpiration">Set time expiration for this key on cache</param>
    /// <returns>A generic T class</returns>
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