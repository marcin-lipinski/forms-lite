using Core.Entities.User;
using Core.Exceptions.Security;
using FastEndpoints;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Services.Interfaces;

namespace Web.Handlers.UserHandlers.Current;

public class CurrentUserHandler : Endpoint<EmptyRequest, CurrentUserResponse>
{
    public IDbContext DbContext { get; set; } = null!;
    public IUserAccessor UserAccessor { get; set; } = null!;
    public ITokenService TokenService { get; set; } = null!;

    public override void Configure()
    {
        Get("/api/user/current");
        AllowAnonymous();
    }

    public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
    {
        var userId = UserAccessor.GetUserId();
        if (userId is null) throw new UnauthorizedException();
        
        var user = await DbContext
            .Collection<User>().AsQueryable()
            .SingleOrDefaultAsync(u => u.Id == userId, cancellationToken: ct);

        await SendAsync(new CurrentUserResponse
        {
            Username = user.Username,
            Token = TokenService.CreateToken(user)
        }, cancellation: ct);
    }
}