using UrlShortener.Application.DTO.IpStack;

namespace UrlShortener.Application.Interfaces;
public interface IIpStackService
{
    Task<LocationInfoDTO> GetCountryByIpAsync(string ip);
}
