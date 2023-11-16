using System.Net;

namespace Core.Exceptions;

public class VException : CustomException
{
    public override HttpStatusCode Code => HttpStatusCode.BadRequest;
    public IDictionary<string, string[]> Errors { get; set; }
    
    public VException(IDictionary<string, string[]> errors, string message = "Errors occured") : base(message)
    {
        Errors = errors;
    }
}