using UrlShortener.Domain.DTO;

namespace UrlShortener.Domain.Services.Interface;
public interface ITokenService
{
    public string GenerateToken(UserDto userDto);
}
