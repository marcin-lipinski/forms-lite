using Core.Entities.Quiz;
using Core.Entities.Session;
using Core.Exceptions.Quiz;
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
    }

    public override async Task HandleAsync(FinishSessionRequest request, CancellationToken cancellationToken)
    {
        var userId = UserAccessor.GetUserId();

        var session = await DbContext
            .Collection<Session>()
            .Find(s => s.Id == request.SessionId).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        var quiz = await DbContext
            .Collection<Quiz>()
            .Find(s => s.AuthorId == userId && s.Id == session.QuizId).SingleOrDefaultAsync(cancellationToken: cancellationToken);

        if (quiz is null) throw new NotFoundException("Session");
        
        session.IsFinishedByAuthor = true;
        await DbContext.Collection<Session>().ReplaceOneAsync(s => s.Id == session.Id, session, cancellationToken: cancellationToken);
        await SendOkAsync(cancellationToken);
    }
}