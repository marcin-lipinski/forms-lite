using Core.Entities.User;

namespace Services.Interfaces;

public interface IPasswordManager
{
    string HashPassword(string password);
    bool VerifyPassword(User user, string hashedPassword, string providedPassword);
}