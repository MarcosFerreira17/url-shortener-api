using System;
using System.Formats.Asn1;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UrlShortener.Domain.DTO;
using UrlShortener.Domain.Services.Interface;

namespace UrlShortener.Domain.Services;
public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(UserDto userDto)
    {
        string issuer = _configuration["Jwt:Issuer"];
        string audience = _configuration["Jwt:Audience"];
        byte[] key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256);

        var subject = new ClaimsIdentity(new Claim[]
        {
            new(JwtRegisteredClaimNames.Sub, userDto.Username),
            new(JwtRegisteredClaimNames.Email, userDto.Email),
        });

        DateTime expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpiresInMinutes"]));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = subject,
            Expires = expires,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);

        return jwtToken;
    }
}
