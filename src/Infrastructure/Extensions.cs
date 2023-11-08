using FastEndpoints.Security;
using Infrastructure.Auth;
using Infrastructure.Persistence;

namespace Infrastructure;

public static class Extensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<TokenSettings>(config.GetSection("Token"));
        services.Configure<MongoDbSettings>(config.GetSection("MongoDatabase"));
        
        services.AddSingleton(typeof(IMongoService<>), typeof(MongoDbService<>));
        //services.AddSingleton<IMongoService<Quiz>, MongoDbService<Quiz>>();
        services.AddJWTBearerAuth(config.GetSection("Token").Get<TokenSettings>().Key);
    }
}