using System.Net;

namespace Core.Exceptions.Security;

public class UnauthorizedException : CustomException
{
    public override HttpStatusCode Code => HttpStatusCode.Unauthorized;
    public UnauthorizedException(string message = "Unauthorized") : base(message)
    { }
}