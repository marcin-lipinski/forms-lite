using Core.Entities.User;
using Core.Exceptions.User;
using FastEndpoints;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Services.Interfaces;

namespace Web.Handlers.UserHandlers.Login;

public class UserLoginEndpoint : Endpoint<UserLoginRequest, UserLoginResponse>
{
    public IDbContext DbContext { get; set; } = null!;
    public IPasswordManager PasswordManager { get; set; } = null!;
    public ITokenService TokenService { get; set; } = null!;

    public override void Configure()
    {
        Post("/api/user/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UserLoginRequest request, CancellationToken cancellationToken)
    {
        var user = await DbContext
            .Collection<User>().AsQueryable()
            .SingleOrDefaultAsync(u => u.Username.Equals(request.Username), cancellationToken: cancellationToken);

        if (user is null) throw new FailedToLogIn();
        if (!PasswordManager.VerifyPassword(user, user.PasswordHashed, request.Password)) throw new FailedToLogIn();

        await SendAsync(new UserLoginResponse
        {
            Username = user.Username,
            Token = TokenService.CreateToken(user)
        }, cancellation: cancellationToken);
    }
}