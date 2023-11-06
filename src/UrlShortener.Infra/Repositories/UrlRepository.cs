using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UrlShortener.Domain.Url.Entities;
using UrlShortener.Domain.Url.Repositories.Interfaces;
using UrlShortener.Infra.Configurations;

namespace UrlShortener.Infra.Repositories;
public class UrlRepository : BaseRepository<ShortUrl>, IUrlRepository
{
    private readonly IMongoCollection<ShortUrl> _collection;
    public UrlRepository(IOptions<MongoSettings> databaseSettings) : base(databaseSettings)
    {
    }

    public async Task<ShortUrl> GetByFilterAsync(FilterDefinition<ShortUrl> filter)
    {
        var result = await _collection.FindAsync(filter);

        return result.FirstOrDefault();
    }
}
