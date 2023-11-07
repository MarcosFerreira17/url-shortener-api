namespace UrlShortener.Application.Helpers;
public static class EnvironmentProperties
{
    public static string GetApplicationUrl()
    {
        string applicationUrl = Environment.GetEnvironmentVariable("ASPNETCORE_URL");

        if (string.IsNullOrEmpty(applicationUrl))
            throw new ArgumentNullException(nameof(applicationUrl));

        return applicationUrl;
    }
}
