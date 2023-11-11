using Core.Entities.Session;
using Core.Exceptions.Quiz;
using Core.Exceptions.Session;
using FastEndpoints;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Services.Interfaces;

namespace Web.Handlers.SessionHandlers.PartakeResult;

public class PartakeSessionResultEndpoint : Endpoint<PartakeSessionResultRequest, EmptyResponse, PartakeSessionResultMapper>
{
    public IDbContext DbContext { get; set; } = null!;

    public override void Configure()
    {
        Post("/api/session/partake/finish/{SessionId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(PartakeSessionResultRequest request, CancellationToken cancellationToken)
    {
        var session = await DbContext.Collection<Session>().AsQueryable()
            .SingleOrDefaultAsync(s => s.Id.Equals(request.SessionId), cancellationToken: cancellationToken);
        if (session is null) throw new NotFoundException("Session");
        if (!session.IsActive()) throw new SessionNotActiveException();

        var answers = Map.ToEntity(request);
        session.SessionAnswers.Add(answers);
        await DbContext.Collection<Session>().ReplaceOneAsync(s => s.Id.Equals(session.Id), session, cancellationToken: cancellationToken);
        
        await SendOkAsync(cancellation: cancellationToken);
    }
}