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

    public async Task<Url> Get(string id) =>
        await _Urls.Find(m => m.Id == id).FirstOrDefaultAsync();

    public async Task Create(Url newEntity)
    {
        Url url = new()
        {
            OriginalUrl = newEntity.OriginalUrl,
            Hash = Url.GenerateHash(newEntity.OriginalUrl)
        };

        await _Urls.InsertOneAsync(url);
    }

    public async Task Update(string id, Url updateEntity) =>
        await _Urls.ReplaceOneAsync(m => m.Id == id, updateEntity);

    public async Task Remove(string id) =>
        await _Urls.DeleteOneAsync(m => m.Id == id);
}
