using System.Net;

namespace Core.Exceptions;

public class CustomException : Exception
{
    public virtual HttpStatusCode Code { get; set; }
    public CustomException(string message) : base(message)
    {}
}