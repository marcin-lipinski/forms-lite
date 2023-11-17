using Core.Entities.Quiz;
using Core.Entities.Session;
using Core.Exceptions.Quiz;
using Core.Exceptions.Session;
using FastEndpoints;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Services.Interfaces;

namespace Web.Handlers.SessionHandlers.Partake;

public class PartakeSessionEndpoint : Endpoint<PartakeSessionRequest, PartakeSessionResponse, PartakeSessionMapper>
{
    public IDbContext DbContext { get; set; } = null!;

    public override void Configure()
    {
        Post("/api/session/partake/start/{SessionId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(PartakeSessionRequest request, CancellationToken cancellationToken)
    {
        var session = await DbContext.Collection<Session>().AsQueryable()
            .SingleOrDefaultAsync(s => s.Id == request.SessionId, cancellationToken: cancellationToken);
        if (session is null) throw new NotFoundException("Session");
        if (session.IsFinishedByAuthor && DateTime.Now > session.StartTime && DateTime.Now < session.FinishTime) throw new SessionNotActiveException();
        
        var quiz = await DbContext.Collection<Quiz>().AsQueryable()
            .SingleOrDefaultAsync(quiz => quiz.Id.Equals(session.QuizId), cancellationToken: cancellationToken);
        var response = Map.FromEntity(quiz);
        
        await SendAsync(response, cancellation: cancellationToken);
    }
}