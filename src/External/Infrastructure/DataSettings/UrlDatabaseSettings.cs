using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure.DataSettings;

public class UrlDatabaseSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string UrlCollectionName { get; set; } = string.Empty;
}
