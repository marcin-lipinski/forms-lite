using Core.Entities.Session;

namespace Web.Handlers.SessionHandlers.PartakeSessionFinish;

public class PartakeSessionFinishRequest
{
    public string SessionId { get; set; } = null!;
    public string ParticipantName { get; set; } = null!;
    public List<SessionPartakeAnswer> Answers { get; set; } = null!;
}