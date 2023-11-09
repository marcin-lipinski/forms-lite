using System.Net;

namespace Core.Exceptions.User;

public class UsernameTakenException : CustomException
{
    public override HttpStatusCode Code => HttpStatusCode.Forbidden;

    public UsernameTakenException(string message) : base(message)
    {
    }
}