using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities.Session;

public class Session : IMongoEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    public string QuizId { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public DateTime FinishTime { get; set; }
    public bool IsFinishedByAuthor { get; set; }
    public string PartakeUrl { get; set; } = null!;
    public bool IsActive => !IsFinishedByAuthor && (DateTime.Now > StartTime && DateTime.Now < FinishTime);
    public readonly List<SessionPartake> SessionAnswers = new ();
}