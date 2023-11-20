using Core.Entities.Question;

namespace Web.Handlers.QuizHandlers.Create;

public class CreateQuizRequest
{
    public QuizDto Quiz { get; set; } = new ();
}

public class QuizDto
{
    public string Title { get; set; } = null!;
    public QuestionDto[] Questions { get; set; } = Array.Empty<QuestionDto>();
}

public class QuestionDto
{
    public string Id { get; set; } = null!;
    public string ContentText { get; set; } = null!;
    public IFormFile? Image { get; set; }  = null!;
    public QuestionType QuestionType { get; set; }
    
    public string[]? Answers { get; set; } = Array.Empty<string>();
    public int? CorrectAnswer { get; set; }
}