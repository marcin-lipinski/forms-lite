using Core.Entities.Question;

namespace Web.Handlers.QuizHandlers.Create;

public class CreateQuizRequest
{
    public QuizDto Quiz { get; set; } = null!;
}

public class QuizDto
{
    public string Title { get; set; } = null!;
    public List<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
}

public class QuestionDto
{
    public string ContentText { get; set; } = null!;
    public IFormFile? Image { get; set; } = null!;
    public QuestionType QuestionType { get; set; }
    public int QuestionNumber { get; set; }
    public List<string>? Answers { get; set; } = new List<string>();
    public int CorrectAnswer { get; set; }
}