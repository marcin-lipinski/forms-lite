using FastEndpoints.Security;
using Server;
using Server.Infrastructure.Persistence;

namespace Infrastructure;

public static class Extensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<TokenSettings>(config.GetSection("Token"));
        services.Configure<MongoDbSettings>(config.GetSection("MongoDatabase"));
        
        services.AddJWTBearerAuth(config.GetSection("Token").Get<TokenSettings>().Key);
    }
}