using Core.DataAccess;
using FastEndpoints;
using Services.Security;

namespace Web.Features.Session.Finish;

public class FinishSessionEndpoint : Endpoint<FinishSessionRequest, FinishSessionResponse>
{
    public IRepository<Core.Entities.Session> SessionRepository { get; set; } = null!;
    public IUserAccessor UserAccessor { get; set; } = null!;

    public override void Configure()
    {
        Post("/api/session/finish/{SessionID}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(FinishSessionRequest request, CancellationToken cancellationToken)
    {
        //var userId = UserAccessor.GetUserId();
        //if (string.IsNullOrEmpty(userId)) throw new UnauthorizedException();
        //if (await IsQuizTitleTaken(request.Quiz.Title, userId)) throw new QuizTitleTakenException();

        var session = await SessionRepository.GetOneAsync(request.SessionId);
        session.IsFinishedByAuthor = true;
        await SessionRepository.UpdateAsync(session);
        await SendAsync(new FinishSessionResponse{Success = true}, cancellation: cancellationToken);
    }
}