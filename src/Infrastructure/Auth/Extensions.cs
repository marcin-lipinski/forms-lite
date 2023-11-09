using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Services.Security;

namespace Infrastructure.Auth;

public static class Extensions
{
    public static void AddSecurity(this IServiceCollection services)
    {
        services
            .AddSingleton<IPasswordManager, PasswordManager>()
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddSingleton<ITokenService, TokenService>();
    }
}