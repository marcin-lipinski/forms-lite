using Core.Entities;

namespace Services.Security;

public interface IPasswordManager
{
    string HashPassword(User user, string password);
    bool VerifyPassword(User user, string hashedPassword, string providedPassword);
}