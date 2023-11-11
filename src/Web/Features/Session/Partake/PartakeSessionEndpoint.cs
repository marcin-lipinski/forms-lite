using Core.DataAccess;
using FastEndpoints;

namespace Web.Features.Session.Partake;

public class PartakeSessionEndpoint : Endpoint<PartakeSessionRequest, PartakeSessionResponse, PartakeSessionMapper>
{
    public IRepository<Core.Entities.Quiz> QuizRepository { get; set; } = null!;
    public IRepository<Core.Entities.Session> SessionRepository { get; set; } = null!;

    public override void Configure()
    {
        Post("/api/session/partake/start/{SessionId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(PartakeSessionRequest request, CancellationToken cancellationToken)
    {
        var session = await SessionRepository.GetOneAsync(request.SessionId);
        if (session is null) throw new Exception("No session");
        if (!session.IsActive) throw new Exception("Sessions finished");
        
        var quiz = await QuizRepository.GetOneAsync(session.QuizId);
        var response = Map.FromEntity(quiz);
        
        await SendAsync(response, cancellation: cancellationToken);
    }
}