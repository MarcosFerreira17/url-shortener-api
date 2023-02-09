using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace UrlShortenerAPI.Data;

public class UrlService
{
    private readonly IMongoCollection<Url> _urls;
    public UrlService(IOptions<UrlShortenerDatabaseSettings> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionString);

        _urls = mongoClient.GetDatabase(options.Value.DatabaseName)
            .GetCollection<Url>(options.Value.UrlsCollectionName);
    }

    public async Task<List<Url>> Get() =>
        await _urls.Find(_ => true).ToListAsync();

    public async Task<Url> Get(string hash) =>
        await _urls.Find(m => m.Hash == hash).FirstOrDefaultAsync();

    public async Task<string> Create(Url newEntity)
    {
        if (!Url.CheckUrl(newEntity.OriginalUrl))
            throw new Exception($"Url não válido: {newEntity}");

        Url url = new()
        {
            OriginalUrl = newEntity.OriginalUrl,
            Hash = Url.GenerateHash(newEntity.OriginalUrl)
        };
        await _urls.InsertOneAsync(url);
        return "http://localhost:5003/url/" + url.Hash;
    }
    public async Task Remove(string id) =>
        await _urls.DeleteOneAsync(m => m.Id == id);
}
