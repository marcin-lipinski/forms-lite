using Core.Entities.User;

namespace Services.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}