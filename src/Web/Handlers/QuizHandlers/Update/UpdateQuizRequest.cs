using Core.Entities.Question;

namespace Web.Handlers.QuizHandlers.Update;

public class UpdateQuizRequest
{
    public string QuizId { get; set; } = null!;
    public bool ReplacePreviousVersion { get; set; } = false;
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
    public IFormFile Image { get; set; } = null!;
    public virtual QuestionType QuestionType { get; set; }
    public int QuestionNumber { get; set; }
    public List<string>? Answers { get; set; } = new ();
    public string CorrectAnswer { get; set; }
}