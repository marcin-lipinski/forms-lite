using Core.DataAccess;
using Core.Exceptions.Security;
using FastEndpoints;
using Services.Security;

namespace Web.Features.User.Login;

public class UserLoginEndpoint : Endpoint<UserLoginRequest, UserLoginResponse>
{
    public IRepository<Core.Entities.User> UserRepository { get; set; } = null!;
    public IPasswordManager PasswordManager { get; set; } = null!;
    public ITokenService TokenService { get; set; } = null!;

    public override void Configure()
    {
        Post("/api/user/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UserLoginRequest request, CancellationToken cancellationToken)
    {
        var users = await UserRepository.GetAllAsync();
        var user = users.SingleOrDefault(u => u.Username.Equals(request.Username));

        if (user is null) throw new UnauthorizedException("Failed to log in. Check the entered data.");
        if (!PasswordManager.VerifyPassword(user, user.PasswordHashed, request.Password)) throw new UnauthorizedException("Failed to log in. Check the entered data.");

        await SendAsync(new UserLoginResponse
        {
            Username = user.Username,
            Token = TokenService.CreateToken(user)
        }, cancellation: cancellationToken);
    }
}