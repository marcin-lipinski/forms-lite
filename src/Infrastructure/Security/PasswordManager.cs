using Core.Entities.User;
using Microsoft.AspNetCore.Identity;
using Services.Interfaces;

namespace Infrastructure.Security;

public class PasswordManager : IPasswordManager
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public PasswordManager(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }
    
    public string HashPassword(string password)
    {
        return _passwordHasher.HashPassword(default, password);
    }

    public bool VerifyPassword(User user, string hashedPassword, string providedPassword)
    {
        return _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword) ==
            PasswordVerificationResult.Success;
    }
}