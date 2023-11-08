namespace Infrastructure.Persistence;

public interface IMongoService<T>
{
    Task<List<T>> GetAllAsync();

    Task<T> GetOneAsync(string id);

    Task CreateAsync(T user);

    Task UpdateAsync(T user);

    Task RemoveAsync(string id);
}