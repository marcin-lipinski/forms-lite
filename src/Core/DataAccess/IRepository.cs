namespace Core.DataAccess;

public interface IRepository<T>
{
    Task<List<T>> GetAllAsync();

    Task<T> GetOneAsync(string id);

    Task<string> CreateAsync(T user);

    Task UpdateAsync(T user);

    Task RemoveAsync(string id);
}