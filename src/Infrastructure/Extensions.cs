using Core.DataAccess;
using FastEndpoints.Security;
using Infrastructure.Auth;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Files;

namespace Infrastructure;

public static class Extensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<TokenSettings>(config.GetSection("TokenSettings"));
        services.Configure<MongoSettings>(config.GetSection("MongoDbSettings"));
        services.Configure<FilesSettings>(config.GetSection("FilesSettings"));
        
        services.AddSingleton(typeof(IRepository<>), typeof(MongoRepository<>));
        //services.AddSingleton<IMongoService<Quiz>, MongoDbService<Quiz>>();
        services.AddJWTBearerAuth(config.GetSection("TokenSettings").Get<TokenSettings>().Key);
        services.AddSecurity();
    }

    public static void UseInfrastructure(this WebApplication app, IConfiguration config)
    {
        app.UseFilesConfiguration(config);
    }
}