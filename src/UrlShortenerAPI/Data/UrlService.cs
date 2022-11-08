using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace UrlShortenerAPI.Data;

public class UrlService
{
    private readonly IMongoCollection<Url> _Urls;

    public UrlService(IOptions<UrlShortenerDatabaseSettings> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionString);

        _Urls = mongoClient.GetDatabase(options.Value.DatabaseName)
            .GetCollection<Url>(options.Value.UrlsCollectionName);
    }

    public async Task<List<Url>> Get() =>
        await _Urls.Find(_ => true).ToListAsync();

    public async Task<Url> Get(string hash) =>
        await _Urls.Find(m => m.Hash == hash).FirstOrDefaultAsync();

    public async Task Create(Url newEntity)
    {
        Url.CheckUrl(newEntity.OriginalUrl);

        Url url = new()
        {
            OriginalUrl = newEntity.OriginalUrl,
            Hash = Url.GenerateHash(newEntity.OriginalUrl)
        };
        await _Urls.InsertOneAsync(url);
    }
    public async Task Remove(string id) =>
        await _Urls.DeleteOneAsync(m => m.Id == id);
}
