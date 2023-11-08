namespace Server.Domain.Entities;

public class Question
{
    public string ContentText { get; set; } = null!;
    public string ContentImageUrl { get; set; } = null!;
    public QuestionType QuestionType { get; set; }
    public string QuestionNumber { get; set; } = null!;
}