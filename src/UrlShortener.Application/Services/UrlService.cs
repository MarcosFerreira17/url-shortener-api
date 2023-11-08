using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Newtonsoft.Json;
using UrlShortener.Application.DTO;
using UrlShortener.Application.Helpers;
using UrlShortener.Application.Interfaces;
using UrlShortener.Domain.Url.Entities;
using UrlShortener.Domain.Url.Repositories.Interfaces;

namespace UrlShortener.Application.Services;
public class UrlService : IUrlService
{
    private readonly IUrlRepository _urlRepository;
    private readonly ILogger<UrlService> _logger;
    private readonly IDistributedCache _cache;
    public UrlService(IUrlRepository urlRepository, ILogger<UrlService> logger, IDistributedCache cache)
    {
        _urlRepository = urlRepository ?? throw new ArgumentNullException(nameof(urlRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }
    public async Task<ShortUrlDTO> CreateShortUrlAsync(UrlDTO urlDTO)
    {
        ShortUrl shortUrl = new(DateTime.Now, urlDTO.Url);

        await _urlRepository.InsertOneAsync(shortUrl);

        string applicationUrl = EnvironmentProperties.GetApplicationUrl();

        return new ShortUrlDTO
        {
            ShortUrl = $"{applicationUrl}/{shortUrl.Text}",
            Hash = shortUrl.Text,
            OriginalUrl = shortUrl.LongUrlText,
            CreatedAt = shortUrl.CreatedAt
        };
    }

    public async Task<string> GetUrlAsync(string hash)
    {
        var cacheKey = "shortUrl";

        ShortUrl urlShort = new();

        var redisShortUrl = await _cache.GetAsync(cacheKey);

        string serializedShortUrl;

        if (redisShortUrl is not null)
        {
            serializedShortUrl = Encoding.UTF8.GetString(redisShortUrl);

            ShortUrl urlDeserialize = JsonConvert.DeserializeObject<ShortUrl>(serializedShortUrl);

            return urlDeserialize.LongUrlText;
        }
        else
        {
            FilterDefinition<ShortUrl> filter = Builders<ShortUrl>.Filter.Eq(x => x.Text, hash);

            ShortUrl shortUrl = await _urlRepository.GetByFilterAsync(filter);

            serializedShortUrl = JsonConvert.SerializeObject(shortUrl);

            redisShortUrl = Encoding.UTF8.GetBytes(serializedShortUrl);

            var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                                                            .SetSlidingExpiration(TimeSpan.FromMinutes(2));

            await _cache.SetAsync(cacheKey, redisShortUrl, options); ;

            if (shortUrl is null)
                return null;

            return shortUrl.LongUrlText;
        }
    }
}
