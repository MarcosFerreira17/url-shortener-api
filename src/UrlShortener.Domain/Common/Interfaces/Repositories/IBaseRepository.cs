using System.Threading.Tasks;
using MongoDB.Driver;

namespace UrlShortener.Domain.Common.Interfaces.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task InsertOneAsync(TEntity document);
    Task UpdateOneAsync(FilterDefinition<TEntity> filter, TEntity document);
    Task DeleteOneAsync(string id);
}
