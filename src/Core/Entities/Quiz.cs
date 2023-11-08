using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Server.Domain.Entities;

public class Quiz
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    public string AuthorId { get; set; } = null!;
    public List<Question> Questions { get; set; } = new List<Question>();
}