using Core.Entities.Session;
using FastEndpoints;
using MongoDB.Driver;
using Services.Interfaces;

namespace Web.Handlers.SessionHandlers.Finish;

public class FinishSessionEndpoint : Endpoint<FinishSessionRequest, EmptyResponse>
{
    public IDbContext DbContext { get; set; } = null!;
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

        var session = await DbContext
            .Collection<Session>()
            .Find(s => s.Id == request.SessionId).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        session.IsFinishedByAuthor = true;
        await DbContext.Collection<Session>().ReplaceOneAsync(s => s.Id == session.Id, session, cancellationToken: cancellationToken);
        await SendOkAsync(cancellationToken);
    }
}