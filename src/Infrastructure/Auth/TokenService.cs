using System.Security.Claims;
using Core.Entities;
using FastEndpoints.Security;
using Microsoft.Extensions.Options;

namespace Infrastructure.Auth;

public class TokenService
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