using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UrlShortener.Domain.Url.Entities;
using UrlShortener.Domain.Url.Interface.Repositories;
using UrlShortener.Infra.Configurations;

namespace UrlShortener.Infra.Repositories;
public class UrlAccessTraceRepository : BaseRepository<UrlAccessTrace>, IUrlAccessTraceRepository
{
    private readonly IMongoCollection<UrlAccessTrace> _collection;

    public UrlAccessTraceRepository(IOptions<MongoSettings> databaseSettings) : base(databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString) ?? throw new ArgumentNullException(nameof(databaseSettings));

        _collection = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName)
                                    .GetCollection<UrlAccessTrace>(
                                    databaseSettings.Value.CollectionName)
                                    ?? throw new ArgumentNullException(nameof(databaseSettings));
    }

    public async Task<List<UrlAccessTrace>> GetByFilterAsync(FilterDefinition<UrlAccessTrace> filter)
    {
        var result = await _collection.FindAsync(filter);

        return result.ToListAsync().Result;
    }
}
