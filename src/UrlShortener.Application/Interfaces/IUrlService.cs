using UrlShortener.Application.DTO;

namespace UrlShortener.Application.Interfaces;
public interface IUrlService
{
    Task<ShortUrlDTO> CreateShortUrlAsync(UrlDTO url);
    Task<string> GetUrlAsync(string hash);
}
