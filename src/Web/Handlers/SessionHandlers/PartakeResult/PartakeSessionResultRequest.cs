using Core.Entities.Session;

namespace Web.Handlers.SessionHandlers.PartakeResult;

public class PartakeSessionResultRequest
{
    public string SessionId { get; set; } = null!;
    public string Participant { get; set; } = null!;
    public List<SessionPartakeAnswer> Answers { get; set; } = null!;
}