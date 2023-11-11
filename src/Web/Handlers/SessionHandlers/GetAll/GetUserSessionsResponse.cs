namespace Web.Handlers.SessionHandlers.GetAll;

public class GetUserSessionsResponse
{
    public List<SessionDto>? Sessions { get; set; } = null!;
}

public class SessionDto
{
    public string Id { get; set; } = null!;
    public string QuizTitle { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public DateTime FinishTime { get; set; }
    public bool IsActive { get; set; }
    public int AnswersAmount { get; set; }
    public string QuizId { get; set; } = null!;
}