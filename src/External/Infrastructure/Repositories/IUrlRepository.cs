using Domain.Entities;

namespace Infrastructure.Repositories;

public interface IUrlRepository
{
    Task Create(Url entity);
    Task Update(Url entity);
    Task Delete(string path);
    Task<Url> Get(string path);
    Task<IEnumerable<Url>> GetAll();
}
