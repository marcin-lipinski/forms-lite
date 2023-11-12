using Core.Entities.User;
using Core.Exceptions;
using Core.Exceptions.User;
using FastEndpoints;
using FluentValidation;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Services.Interfaces;

namespace Web.Handlers.UserHandlers.Register;

public class UserRegisterEndpoint : Endpoint<UserRegisterRequest, UserRegisterResponse>
{
    public IDbContext DbContext { get; set; } = null!;
    public IPasswordManager PasswordManager { get; set; } = null!;
    public ITokenService TokenService { get; set; } = null!;

    public override void Configure()
    {
        Put("/api/user/register");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(UserRegisterRequest request, CancellationToken cancellationToken)
    {
        var user = await DbContext
            .Collection<User>().AsQueryable()
            .SingleOrDefaultAsync(u => u.Username.Equals(request.Username), cancellationToken: cancellationToken);
        
        if(user is not null) throw new UsernameTakenException();
        
        var hashedPassword = PasswordManager.HashPassword(request.Password);

        var domainUser = new User
        {
            Username = request.Username,
            PasswordHashed = hashedPassword
        };

        await DbContext.Collection<User>().InsertOneAsync(domainUser, cancellationToken: cancellationToken);

        await SendAsync(new UserRegisterResponse
        {
            Username = domainUser.Username,
            Token = TokenService.CreateToken(domainUser)
        }, cancellation: cancellationToken);
    }
}