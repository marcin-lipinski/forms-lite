using System.Net;

namespace Core.Exceptions.Quiz;

public class NotFoundException : CustomException
{
    public override HttpStatusCode Code => HttpStatusCode.NotFound;

    public NotFoundException(string resource) : base($"{resource} not found")
    {
    }
}