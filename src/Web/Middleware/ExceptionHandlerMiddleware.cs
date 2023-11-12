using System.Text.Json;
using Core.Exceptions;
using FluentValidation;

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
        catch (CustomException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)exception.Code;
            
            var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
            var errors = new { error = new string[]{ exception.Message } };
            var message = exception.GetType().Name;

            var json = _env.IsDevelopment()
                ? JsonSerializer.Serialize(new { Errors = errors, Message = message, StatusCode = exception.Code, StackTrace = exception.StackTrace }, options)
                : JsonSerializer.Serialize(new { ErrorCode = errors, Message = message, StatusCode = exception.Code, }, options);
            
            await context.Response.WriteAsync(json);
        }
        catch (Exception exception)
        {
            if(_env.IsDevelopment()) Console.WriteLine(exception);
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            
            var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
            var message = _env.IsDevelopment() ? exception.Message : "Internal Server Error";
            var errorCode = exception.GetType().Name;

            var json = _env.IsDevelopment()
                ? JsonSerializer.Serialize(new { ErrorCode = errorCode, Message = message, StackTrace = exception.StackTrace }, options)
                : JsonSerializer.Serialize(new { ErrorCode = errorCode, Message = message }, options);
            
            await context.Response.WriteAsync(json);
        }
    }
}