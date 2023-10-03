using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using UrlShortener.Infra.Configurations;
using UrlShortener.Infra.Repositories.Interfaces;

namespace UrlShortener.Infra.Repositories;
public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly IMongoCollection<TEntity> _collection;
    public BaseRepository(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(
        databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName);

        _collection = mongoDatabase.GetCollection<TEntity>(
            databaseSettings.Value.CollectionName);
    }

    public virtual async Task DeleteOneAsync(string id)
    {
        await _collection.DeleteOneAsync(id);
    }

    public virtual async Task InsertOneAsync(TEntity document)
    {
        await _collection.InsertOneAsync(document);
    }

    public virtual async Task UpdateOneAsync(FilterDefinition<TEntity> filter, TEntity document)
    {
        await _collection.ReplaceOneAsync(filter, document);
    }
}
