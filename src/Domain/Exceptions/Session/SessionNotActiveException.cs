using System.Net;

namespace Core.Exceptions.Session;

public class SessionNotActiveException : CustomException
{
    public override HttpStatusCode Code => HttpStatusCode.BadRequest;

    public SessionNotActiveException(string message = "Session is not active") : base(message)
    {
    }
}