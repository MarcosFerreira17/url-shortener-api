using Domain.Entities;
using Infrastructure.DataSettings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class UrlRepository : IUrlRepository
{
    private readonly IMongoCollection<Url> _Url;

    public UrlRepository(IOptions<UrlDatabaseSettings> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionString);
        _Url = mongoClient.GetDatabase(options.Value.DatabaseName)
                                .GetCollection<Url>(options.Value.UrlCollectionName);
    }

    public async Task<IEnumerable<Url>> GetAll()
    {
        return await _Url
                        .Find(_ => true)
                        .ToListAsync();
    }
    public async Task<Url> Get(string id)
    {
        return await _Url
                        .Find(p => p.Id == id)
                        .FirstOrDefaultAsync();
    }

    public async Task Create(Url Url)
    {
        await _Url.InsertOneAsync(Url);
    }

    public async Task Update(Url Url)
    {
        await _Url.ReplaceOneAsync(filter: g => g.Id == Url.Id, replacement: Url);
    }

    public async Task Delete(string id)
    {
        FilterDefinition<Url> filter = Builders<Url>.Filter.Eq(p => p.Id, id);
        await _Url.DeleteOneAsync(filter);
    }
}
