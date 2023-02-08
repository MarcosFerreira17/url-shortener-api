namespace UrlShortenerAPI.Data;

public class UrlShortenerDatabaseSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string UrlsCollectionName { get; set; } = string.Empty;
}
