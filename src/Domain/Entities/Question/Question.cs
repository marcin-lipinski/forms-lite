using Core.Entities.Image;

namespace Core.Entities.Question;

public class Question
{
    public string Id { get; set; } = null!;
    public string ContentText { get; init; } = null!;
    public ImageMetadata? Image { get; set; }
    public virtual QuestionType QuestionType { get; set; }
    public List<string>? Answers { get; init; } = new();
    public int? CorrectAnswer { get; init; }
}