using System.Text.Json;
using Core.Exceptions;

namespace Web.Middleware;

public class ExceptionHandlerMiddleware : IMiddleware
{
    private readonly IHostEnvironment _env;
    
    public ExceptionHandlerMiddleware(IHostEnvironment env)
    {
        _env = env;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            if(_env.IsDevelopment()) Console.WriteLine(exception);
            var isCustomException = exception.GetType().IsSubclassOf(typeof(CustomException));
        
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = isCustomException ? (int)((CustomException)exception).Code : 500;
            
            var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
            var message = isCustomException || _env.IsDevelopment() ? exception.Message : "Internal Server Error";
            var errorCode = exception.GetType().Name;

            var json = _env.IsDevelopment()
                ? JsonSerializer.Serialize(new { ErrorCode = errorCode, Message = message, StackTrace = exception.StackTrace }, options)
                : JsonSerializer.Serialize(new { ErrorCode = errorCode, Message = message }, options);
            
            await context.Response.WriteAsync(json);
        }
    }
}