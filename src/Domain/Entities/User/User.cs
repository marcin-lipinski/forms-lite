using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities.User;

public class User : IMongoEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    public string Username { get; set; } = null!;
    public string PasswordHashed { get; set; } = null!;
}