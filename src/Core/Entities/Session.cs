using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Server.Domain.Entities;

public class Session
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    public string QuizId { get; set; } = null!;

    public string ParticipantName { get; set; } = null!;
    public List<Answer> Answers { get; set; } = new List<Answer>();
}