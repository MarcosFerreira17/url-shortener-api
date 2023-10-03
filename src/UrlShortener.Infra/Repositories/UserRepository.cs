using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UrlShortener.Domain.Entities;
using UrlShortener.Infra.Configurations;
using UrlShortener.Infra.Repositories.Interfaces;

namespace UrlShortener.Infra.Repositories;
public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly IMongoCollection<User> _collection;
    public UserRepository(IOptions<DatabaseSettings> databaseSettings) : base(databaseSettings) { }

    public async Task<User> GetByFilterAsync(FilterDefinition<User> filter)
    {
        var result = await _collection.FindAsync(filter);

        return result.FirstOrDefault();
    }

    public async Task<User> GetByIdAsync(string id)
    {
        var result = await _collection.FindSync(x => x.Id == id).FirstOrDefaultAsync();

        return result;
    }
}
