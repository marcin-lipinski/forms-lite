using Core;
using MongoDB.Driver;

namespace Services.Interfaces;

public interface IDbContext
{
    IMongoCollection<T> Collection<T>()  where T : IMongoEntity;
}