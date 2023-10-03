using System.Threading.Tasks;
using MongoDB.Driver;
using UrlShortener.Domain.DTO;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Infra.Repositories.Interfaces;
public interface IUserRepository : IBaseRepository<User>
{
    Task<User> GetByFilterAsync(FilterDefinition<User> filter);
    Task<User> GetByIdAsync(string id);
}
