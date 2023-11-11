namespace Core.Entities.Session;

public class SessionPartake
{
    public string ParticipantName { get; set; } = null!;
    public List<SessionPartakeAnswer> Answers { get; set; } = new();
}