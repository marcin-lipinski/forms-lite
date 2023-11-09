namespace Core.Entities;

public class QuestionOpen : Question
{
    public override QuestionType QuestionType => QuestionType.Open;
    public List<string>? Answers { get; set; } = new List<string>();
    public int CorrectAnswer { get; set; }
}