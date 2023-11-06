namespace UrlShortener.Infra.Configurations;
public class MongoSettings
{
    public string ConnectionString { get; set; } = string.Empty;

    public string DatabaseName { get; set; } = string.Empty;

    public string CollectionName { get; set; } = string.Empty;
}
