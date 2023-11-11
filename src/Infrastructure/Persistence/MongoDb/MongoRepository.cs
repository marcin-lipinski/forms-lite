using Core;
using Core.DataAccess;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Persistence.MongoDb;

public class MongoRepository<T> : IRepository<T> where T : IMongoEntity
{
    private readonly IMongoCollection<T> _collection;

    public MongoRepository(IOptions<MongoSettings> dbSettings)
    {
        var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
        _collection = mongoDb.GetCollection<T>(dbSettings.Value.CollectionName[typeof(T).Name]);
    }

    public async Task<List<T>> GetAllAsync() =>
        await _collection.Find(_ => true).ToListAsync();

    public async Task<T> GetOneAsync(string id) =>
        await _collection.Find(u => u.Id.Equals(id)).SingleOrDefaultAsync();

    public async Task<string> CreateAsync(T entity)
    {
        entity.Id = ObjectId.GenerateNewId().ToString();
        await _collection.InsertOneAsync(entity);
        return entity.Id;
    }

    public async Task UpdateAsync(T entity) =>
        await _collection.ReplaceOneAsync(u => u.Id.Equals(entity.Id), entity);

    public async Task RemoveAsync(string id) =>
        await _collection.DeleteOneAsync(u => u.Id.Equals(id));

}