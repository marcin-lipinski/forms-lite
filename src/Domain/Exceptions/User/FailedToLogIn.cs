using System.Net;

namespace Core.Exceptions.User;

public class FailedToLogIn : CustomException
{
    public override HttpStatusCode Code => HttpStatusCode.Unauthorized;

    public FailedToLogIn(string message = "Failed to log in. Check the entered data.") : base(message)
    {
    }
}