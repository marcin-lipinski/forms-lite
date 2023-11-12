using Core.Entities.Image;

namespace Core.Entities.Question;

public class Question
{
    public string ContentText { get; init; } = null!;
    public ImageMetadata? Image { get; set; }
    public virtual QuestionType QuestionType { get; set; }
    public int QuestionNumber { get; init; }
    public List<string>? Answers { get; init; } = new();
    public int? CorrectAnswer { get; init; }
}