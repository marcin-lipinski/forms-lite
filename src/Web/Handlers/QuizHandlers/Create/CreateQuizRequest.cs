using Core.Entities.Question;

namespace Web.Handlers.QuizHandlers.Create;

public class CreateQuizRequest
{
    public QuizDto Quiz { get; set; }
}

public class QuizDto
{
    public string Title { get; set; }
    public QuestionDto[] Questions { get; set; }
}

public class QuestionDto
{
    public string ContentText { get; set; }
    public IFormFile? Image { get; set; }
    public QuestionType QuestionType { get; set; }
    public int QuestionNumber { get; set; }
    public string[]? Answers { get; set; }
    public string CorrectAnswer { get; set; }
}