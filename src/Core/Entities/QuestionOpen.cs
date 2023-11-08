namespace Core.Entities;

public class QuestionOpen
{
    public QuestionType QuestionType = QuestionType.Open;
    public List<string> Answers { get; set; } = new List<string>();
    public int CorrectAnswer { get; set; }
}