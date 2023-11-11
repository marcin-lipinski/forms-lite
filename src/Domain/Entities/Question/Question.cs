using Core.Entities.Image;

namespace Core.Entities.Question;

public class Question
{
    public string ContentText { get; init; } = null!;
    public ImageMetadata Image { get; set; } = null!;
    public virtual QuestionType QuestionType { get; set; }
    public int QuestionNumber { get; init; }
}