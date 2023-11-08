using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Server.Application.Services;
using Server.Domain;

namespace Server.Infrastructure.Persistence;

public class MongoDbService<T> : IService<T> where T : IMongoEntity
{
    private readonly IMongoCollection<T> _collection;

    public MongoDbService(IOptions<MongoDbSettings> dbSettings)
    {
        var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
        _collection = mongoDb.GetCollection<T>(dbSettings.Value.CollectionName[nameof(T)]);
    }

    public async Task<List<T>> GetAllAsync() =>
        await _collection.Find(_ => true).ToListAsync();

    public async Task<T> GetOneAsync(string id) =>
        await _collection.Find(u => u.Id.Equals(id)).SingleOrDefaultAsync();

    public async Task CreateAsync(T entity) =>
        await _collection.InsertOneAsync(entity);

    public async Task UpdateAsync(T entity) =>
        await _collection.ReplaceOneAsync(u => u.Id.Equals(entity.Id), entity);

    public async Task RemoveAsync(string id) =>
        await _collection.DeleteOneAsync(u => u.Id.Equals(id));

}