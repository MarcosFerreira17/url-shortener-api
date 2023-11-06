using System.Threading.Tasks;
using MongoDB.Driver;
using UrlShortener.Domain.Common.Interfaces.Repositories;
using UrlShortener.Domain.Url.Entities;

namespace UrlShortener.Domain.Url.Repositories.Interfaces;

public interface IUrlRepository : IBaseRepository<ShortUrl>
{
    Task<ShortUrl> GetByFilterAsync(FilterDefinition<ShortUrl> filter);
}