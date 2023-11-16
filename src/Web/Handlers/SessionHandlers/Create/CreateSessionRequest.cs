namespace Web.Handlers.SessionHandlers.Create;

public class CreateSessionRequest
{
    public string QuizId { get; set; } = null!;
    public string StartTime { get; set; }
    public string FinishTime { get; set; }
}