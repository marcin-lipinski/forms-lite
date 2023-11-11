namespace Core.Entities.Question;

public class QuestionOpen : Question
{
    public override QuestionType QuestionType => QuestionType.Open;
    public List<string>? Answers { get; init; } = new();
    public int CorrectAnswer { get; init; }
}