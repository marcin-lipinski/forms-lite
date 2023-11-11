using System.Security.Claims;
using Core.Entities.User;
using FastEndpoints.Security;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Services.Interfaces;

namespace Infrastructure.Security;

public class TokenService : ITokenService
{
    private readonly IOptions<TokenSettings> _tokenSettings;

    public TokenService(IOptions<TokenSettings> tokenSettings)
    {
        _tokenSettings = tokenSettings;
    }
    
    public string CreateToken(User user)
    {
        return JWTBearer.CreateToken
        (
            signingKey: _tokenSettings.Value.Key,
            expireAt: DateTime.Now.Add(TimeSpan.Parse(_tokenSettings.Value.ExpireTime)),
            privileges: p =>
            {
                p.Claims.AddRange(new[]
                    { new Claim(ClaimTypes.NameIdentifier, user.Id), new Claim("Username", user.Username) });
            }
        );
    }
}