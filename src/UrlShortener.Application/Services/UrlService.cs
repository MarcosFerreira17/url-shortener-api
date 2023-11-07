using Microsoft.Extensions.Logging;
using MongoDB.Driver;
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
    public UrlService(IUrlRepository urlRepository, ILogger<UrlService> logger)
    {
        _urlRepository = urlRepository ?? throw new ArgumentNullException(nameof(urlRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
        FilterDefinition<ShortUrl> filter = Builders<ShortUrl>.Filter.Eq(x => x.Text, hash);

        ShortUrl shortUrl = await _urlRepository.GetByFilterAsync(filter);

        if (shortUrl is null)
            return null;

        return shortUrl.LongUrlText;
    }
}
