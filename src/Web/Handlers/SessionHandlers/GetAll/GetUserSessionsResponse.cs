using Core.Entities.Session;
using Web.Handlers.QuizHandlers.GetAll;

namespace Web.Handlers.SessionHandlers.GetAll;

public class GetUserSessionsResponse
{
    public List<SessionDto>? Sessions { get; set; } = null!;
}

public class SessionDto
{
    public string Id { get; set; } = null!;
    public string JoinUrl { get; set; } = null!;
    public string QuizTitle { get; set; } = null!;
    public string StartTime { get; set; } = null!;
    public string FinishTime { get; set; } = null!;
    public bool IsActive { get; set; }
    public List<SessionPartake> Answers { get; set; }
    public List<QuestionDto> Questions { get; set; }
    public string QuizId { get; set; } = null!;
}

public class QuestionDto
{
    public string Id { get; set; }
    public string ContentText { get; set; }
}