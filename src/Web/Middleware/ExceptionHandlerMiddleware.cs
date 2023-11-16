using System.Text.Json;
using Core.Exceptions;
using FluentValidation;

namespace Web.Middleware;

public class ExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (VException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)exception.Code;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var errors = exception.Errors;
            var message = exception.GetType().Name;

            var json = JsonSerializer.Serialize(new { errors = errors, message = message, status = exception.Code }, options);

            await context.Response.WriteAsync(json);
        }
        catch (CustomException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)exception.Code;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var errors = new Dictionary<string, string[]>(){{"Errors", new []{exception.Message}}};
            var message = exception.GetType().Name;

            var json = JsonSerializer.Serialize(new { errors = errors, message = message, status = exception.Code}, options);

            await context.Response.WriteAsync(json);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var message = new Dictionary<string, string[]>(){{"Errors", new []{"Internal Server Error"}}};
            var errorCode = exception.GetType().Name;

            var json = JsonSerializer.Serialize(new { ErrorCode = errorCode, Message = message }, options);

            await context.Response.WriteAsync(json);
        }
    }
}