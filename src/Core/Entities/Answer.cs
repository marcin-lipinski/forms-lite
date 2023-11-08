namespace Server.Domain.Entities;

public class Answer
{
    public int QuestionNumber { get; set; }
    public string QuestionAnswer { get; set; } = null!;
}