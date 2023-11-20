using Core.Entities.Question;

namespace Web.Handlers.QuizHandlers.Update;

public class UpdateQuizRequest
{
    public bool ReplacePreviousVersion { get; set; } = false;
    public QuizDto Quiz { get; set; } = null!;
}

public class QuizDto
{
    public string Title { get; set; } = null!;
    public List<QuestionDto> Questions { get; set; } = new ();
}

public class QuestionDto
{
    public string Id { get; set; } = null!;
    public string ContentText { get; set; } = null!;
    public string ContentImageUrl { get; set; } = null!;
    public IFormFile Image { get; set; } = null!;
    public QuestionType QuestionType { get; set; }
    public List<string>? Answers { get; set; } = new ();
    public int? CorrectAnswer { get; set; }
}