namespace Core.Entities.Question;

public class QuestionClosed : Question
{
    public override QuestionType QuestionType => QuestionType.Closed;
}