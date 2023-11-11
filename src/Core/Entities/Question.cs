namespace Core.Entities;

public class Question
{
    public string ContentText { get; set; } = null!;
    public Image Image { get; set; } = null!;
    public virtual QuestionType QuestionType { get; set; }
    public int QuestionNumber { get; set; }
}