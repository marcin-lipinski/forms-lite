using Core.Entities.Question;

namespace Web.Handlers.QuizHandlers.Create;

public class CreateQuizRequest
{
    public QuizDto Quiz { get; set; } = new QuizDto();
}

public class QuizDto
{
    public string Title { get; set; }
    public QuestionDto[] Questions { get; set; } = Array.Empty<QuestionDto>();
}

public class QuestionDto
{
    public string ContentText { get; set; }
    public IFormFile? Image { get; set; }
    public QuestionType QuestionType { get; set; }
    public int QuestionNumber { get; set; }
    public string[]? Answers { get; set; } = Array.Empty<string>();
    public string CorrectAnswer { get; set; }
}