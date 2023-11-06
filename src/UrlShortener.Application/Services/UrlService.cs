using Microsoft.Extensions.Logging;
using MongoDB.Driver;
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
    public async Task<string> CreateShortUrlAsync(string url)
    {
        ShortUrl shortUrl = new(DateTime.Now, url);

        await _urlRepository.InsertOneAsync(shortUrl);

        return shortUrl.Text;
    }

    public async Task<string> GetUrlAsync(string shortUrl)
    {
        var filter = Builders<ShortUrl>.Filter.Eq(x => x.Text, shortUrl);

        var result = await _urlRepository.GetByFilterAsync(filter);

        return result?.Text;
    }
}
