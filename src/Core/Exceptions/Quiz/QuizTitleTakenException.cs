using System.Net;

namespace Core.Exceptions.Quiz;

public class QuizTitleTakenException : CustomException
{
    public override HttpStatusCode Code => HttpStatusCode.BadRequest;

    public QuizTitleTakenException() : base("This quiz title is already in use.")
    {
    }
}