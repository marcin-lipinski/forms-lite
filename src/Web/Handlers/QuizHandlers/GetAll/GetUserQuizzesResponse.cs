using Core.Entities.Question;

namespace Web.Handlers.QuizHandlers.GetAll;

public class GetUserQuizzesResponse
{
    public List<QuizDto> Quizzes { get; init; } = null!;
}

public class QuizDto
{
    public string Id { get; init; } = null!;
    public string Title { get; set; } = null!;
    public List<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
}

public class QuestionDto
{
    public string? ContentImageUrl { get; set; } = null!;
    public string ContentText { get; set; } = null!;
    public QuestionType QuestionType { get; set; }
    public int QuestionNumber { get; set; }
    public List<string>? Answers { get; set; }
    public string? CorrectAnswer { get; set; }
}