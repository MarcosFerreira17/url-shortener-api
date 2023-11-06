using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UrlShortener.Domain.Common.Interfaces.Repositories;
using UrlShortener.Infra.Configurations;

namespace UrlShortener.Infra.Repositories;
public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly IMongoCollection<TEntity> _collection;

    public BaseRepository(IOptions<MongoSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(
        databaseSettings.Value.ConnectionString) ?? throw new ArgumentNullException(nameof(databaseSettings));

        _collection = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName).GetCollection<TEntity>(
            databaseSettings.Value.CollectionName) ?? throw new ArgumentNullException(nameof(databaseSettings));
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
