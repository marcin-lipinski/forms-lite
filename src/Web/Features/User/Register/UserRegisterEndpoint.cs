using Core.DataAccess;
using Core.Exceptions.Security;
using Core.Exceptions.User;
using FastEndpoints;
using Services.Security;
using Web.Features.User.Login;

namespace Web.Features.User.Register;

public class UserRegisterEndpoint : Endpoint<UserRegisterRequest, UserRegisterResponse>
{
    public IRepository<Core.Entities.User> UserRepository { get; set; } = null!;
    public IPasswordManager PasswordManager { get; set; } = null!;
    public ITokenService TokenService { get; set; } = null!;

    public override void Configure()
    {
        Post("/api/user/register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UserRegisterRequest request, CancellationToken cancellationToken)
    {
        var users = await UserRepository.GetAllAsync();
        if(users.SingleOrDefault(u => u.Username.Equals(request.Username)) is not null)
            throw new UsernameTakenException("Failed to register. Provided username is already in use.");
        
        var hashedPassword = PasswordManager.HashPassword(default, request.Password);

        var user = new Core.Entities.User
        {
            Username = request.Username,
            PasswordHashed = hashedPassword
        };

        await UserRepository.CreateAsync(user);

        await SendAsync(new UserRegisterResponse
        {
            Username = user.Username,
            Token = TokenService.CreateToken(user)
        }, cancellation: cancellationToken);
    }
}