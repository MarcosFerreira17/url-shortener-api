using Microsoft.AspNetCore.Http;

namespace UrlShortener.Application.Helpers;
public static class IpAddress
{
    public static string GetIpAddress(this HttpContext httpContext)
    {
        string? ipAddress = httpContext.Connection.RemoteIpAddress?.ToString();

        if (string.IsNullOrEmpty(ipAddress))
            ipAddress = httpContext.Connection.LocalIpAddress?.ToString();

        return ipAddress ?? string.Empty;
    }
}
