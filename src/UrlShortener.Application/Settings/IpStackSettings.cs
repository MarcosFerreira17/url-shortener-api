namespace UrlShortener.Application.Settings;
public class IpStackSettings
{
    public string Url { get; set; } = string.Empty;
    public string AccessKey { get; set; } = string.Empty;
    public int CountUsageLimit { get; set; }
    public int FreeUsageLimit { get; set; } = 1000;
}
