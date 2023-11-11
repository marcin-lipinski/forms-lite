using Core.Entities;

namespace Web.Features.Session.PartakeResult;

public class PartakeSessionResultRequest
{
    public string SessionId { get; set; } = null!;
    public string Participan { get; set; } = null!;
    public List<Answer> Answers { get; set; } = null!;
}

