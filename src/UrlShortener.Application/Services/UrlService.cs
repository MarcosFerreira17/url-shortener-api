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
    private readonly ICacheService _cache;
    public UrlService(IUrlRepository urlRepository, ILogger<UrlService> logger, ICacheService cache)
    {
        _urlRepository = urlRepository ?? throw new ArgumentNullException(nameof(urlRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }
    public async Task<ShortUrlDTO> CreateShortUrlAsync(UrlDTO urlDTO)
    {
        ShortUrl shortUrl = new(DateTime.Now, urlDTO.Url);

        if (await UrlAlreadyExists(shortUrl))
            _logger.LogInformation($"ShortUrl already exists with hash {shortUrl.Hash}");
        else
            await _urlRepository.InsertOneAsync(shortUrl);

        string applicationUrl = EnvironmentProperties.GetApplicationUrl();

        _logger.LogInformation($"ShortUrl created with hash {shortUrl.Hash}");

        return new ShortUrlDTO
        {
            ShortUrl = $"{applicationUrl}/{shortUrl.Hash}",
            Hash = shortUrl.Hash,
            OriginalUrl = shortUrl.LongUrl,
            CreatedAt = shortUrl.CreatedAt
        };
    }

    public async Task<string> GetUrlAsync(string hash)
    {
        FilterDefinition<ShortUrl> filter = Builders<ShortUrl>.Filter.Eq(options => options.Hash, hash);

        ShortUrl shortUrl = null;

        shortUrl = await _cache.GetOrSetInMemory(hash, async () => shortUrl = await _urlRepository.GetByFilterAsync(filter));

        if (shortUrl is null)
            return null;

        await UpdateLastTimeAccess(shortUrl);

        return shortUrl.LongUrl;
    }

    private async Task<bool> UrlAlreadyExists(ShortUrl shortUrl)
    {
        FilterDefinition<ShortUrl> filter = Builders<ShortUrl>.Filter.Eq(options => options.Hash, shortUrl.Hash);

        shortUrl = await _cache.GetOrSetInMemory(shortUrl.Hash, async () => shortUrl = await _urlRepository.GetByFilterAsync(filter));

        if (shortUrl is null)
            return false;

        return true;
    }

    private async Task UpdateLastTimeAccess(ShortUrl shortUrl)
    {
        shortUrl = ShortUrl.UpdateLastAccessedAt(shortUrl);

        FilterDefinition<ShortUrl> filter = Builders<ShortUrl>.Filter.Eq(options => options.Id, shortUrl.Id);

        await _urlRepository.UpdateOneAsync(filter, shortUrl);

        _logger.LogInformation($"Last time accessed updated for {shortUrl.Hash}");
    }
}
