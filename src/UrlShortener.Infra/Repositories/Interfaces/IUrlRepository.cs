using System.Threading.Tasks;
using MongoDB.Driver;
using UrlShortener.Domain.DTO;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Infra.Repositories.Interfaces;

public interface IUrlRepository : IBaseRepository<ShortUrl>
{
    Task<ShortUrl> GetByFilterAsync(FilterDefinition<ShortUrl> filter);
    Task<ShortUrl> GetByIdAsync(string id);
}