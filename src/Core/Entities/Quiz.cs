using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities;

public class Quiz : IMongoEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    public string AuthorId { get; set; } = null!;
    public string Title { get; set; } = null!;
    public int Version { get; set; }
    public List<Question> Questions { get; set; } = new List<Question>();
}