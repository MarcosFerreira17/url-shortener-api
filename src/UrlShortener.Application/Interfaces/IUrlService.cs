namespace UrlShortener.Application.Interfaces;
public interface IUrlService
{
    Task<string> CreateShortUrlAsync(string url);
    Task<string> GetUrlAsync(string shortUrl);
}
