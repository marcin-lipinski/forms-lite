using System.Net;

namespace Core.Exceptions.User;

public class NoCurrentUserException : CustomException
{
    public override HttpStatusCode Code => HttpStatusCode.Unauthorized;
    
    public NoCurrentUserException(string message = "") : base(message)
    {
    }
}