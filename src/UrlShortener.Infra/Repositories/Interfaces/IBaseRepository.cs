using System.Threading.Tasks;
using MongoDB.Driver;

namespace UrlShortener.Infra.Repositories.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task InsertOneAsync(TEntity document);
    Task UpdateOneAsync(FilterDefinition<TEntity> filter, TEntity document);
    Task DeleteOneAsync(string id);
}
