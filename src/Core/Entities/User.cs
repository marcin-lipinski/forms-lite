using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Server.Domain.Entities;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    public string Username { get; set; } = null!;
    public string PasswordHashed { get; set; } = null!;
    public string Email { get; set; } = null!;
}