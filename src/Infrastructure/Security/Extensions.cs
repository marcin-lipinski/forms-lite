using Core.Entities.User;
using Microsoft.AspNetCore.Identity;
using Services.Interfaces;

namespace Infrastructure.Security;

public static class Extensions
{
    public static void AddSecurity(this IServiceCollection services)
    {
        services
            .AddSingleton<IPasswordManager, PasswordManager>()
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddSingleton<ITokenService, TokenService>()
            .AddSingleton<IUserAccessor, UserAccessor>();
    }
}