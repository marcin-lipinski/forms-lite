namespace Core.Entities;

public class QuestionClosed : Question
{
    public override QuestionType QuestionType => QuestionType.Closed;
}