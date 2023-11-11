namespace Web.Features.Session.Create;

public class CreateSessionRequest
{
    public string QuizId { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public DateTime FinishTime { get; set; }
}