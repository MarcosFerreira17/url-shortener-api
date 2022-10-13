namespace Infrastructure.Repositories;

public interface ITRepository<T>
{
    Task Create(T entity);
    Task Update(T entity);
    Task Delete(string path);
    Task<T?> Get(string path);
    Task<IEnumerable<T>> GetAll();
}
