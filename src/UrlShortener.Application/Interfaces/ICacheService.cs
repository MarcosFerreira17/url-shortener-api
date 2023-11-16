using Microsoft.Extensions.Caching.Distributed;

namespace UrlShortener.Application.Interfaces;
public interface ICacheService
{
    Task<T> GetOrSetInMemory<T>(string key, Func<Task<T>> func, TimeSpan? absoluteExpiration = null);
    Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> func, TimeSpan? absoluteExpiration = null);
}