using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities.Quiz;

public class Quiz : IMongoEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    public string AuthorId { get; init; } = null!;
    public string Title { get; set; } = null!;
    public int Version { get; set; }
    public List<Question.Question> Questions { get; init; } = new();
}