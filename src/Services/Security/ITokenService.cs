using Core.Entities;

namespace Services.Security;

public interface ITokenService
{
    string CreateToken(User user);
}