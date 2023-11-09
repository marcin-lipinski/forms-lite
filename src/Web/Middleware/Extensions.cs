namespace Web.Middleware;

public static class Extensions
{
        public static void AddExceptionHandler(this IServiceCollection services)
        {
                services.AddScoped<ExceptionHandlerMiddleware>();
        }
        public static void UseExceptionHandler(this WebApplication app)
        { 
                app.UseMiddleware<ExceptionHandlerMiddleware>();  
        }
}