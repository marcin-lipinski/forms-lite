using Core;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Services.Interfaces;

namespace Infrastructure.Persistence.MongoDb;

public class MongoContext : IDbContext
{
    private readonly IMongoDatabase _mongoDb;
    private readonly IOptions<MongoSettings> _settings;

    public MongoContext(IOptions<MongoSettings> dbSettings)
    {
        _settings = dbSettings;
        var mongoClient = new MongoClient(_settings.Value.ConnectionString);
        _mongoDb = mongoClient.GetDatabase(_settings.Value.DatabaseName);
    }

    public IMongoCollection<T> Collection<T>() where T : IMongoEntity
    {
        return _mongoDb.GetCollection<T>(_settings.Value.CollectionName[typeof(T).Name]);
    }
}