using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities;

public class Session : IMongoEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    public string QuizId { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public DateTime FinishTime { get; set; }
    public bool IsFinishedByAuthor { get; set; }
    public bool IsActive => !IsFinishedByAuthor && (DateTime.Now > StartTime && DateTime.Now < FinishTime);
    public List<SessionAnswer> SessionAnswers = new List<SessionAnswer>();
}

public class SessionAnswer
{
    public string ParticipantName { get; set; } = null!;
    public List<Answer> Answers { get; set; } = new List<Answer>();
}