using UrlShortener.Application.DTO;

namespace UrlShortener.Application.Interfaces;
public interface IUrlAccessTraceService
{
    Task<List<UrlAccessTraceDTO>> GetByAccessByHashAsync(string hash);
    Task InsertAsync(string hash, string ip);
}
