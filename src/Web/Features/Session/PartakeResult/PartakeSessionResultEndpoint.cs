using Core.DataAccess;
using Core.Entities;
using FastEndpoints;
using Services.Security;
using Web.Features.Quiz.Create;

namespace Web.Features.Session.PartakeResult;

public class PartakeSessionResultEndpoint : Endpoint<PartakeSessionResultRequest, EmptyResponse, PartakeSessionResultMapper>
{
    public IRepository<Core.Entities.Quiz> QuizRepository { get; set; } = null!;
    public IRepository<Core.Entities.Session> SessionRepository { get; set; } = null!;

    public override void Configure()
    {
        Post("/api/session/partake/finish/{SessionId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(PartakeSessionResultRequest request, CancellationToken cancellationToken)
    {
        var session = await SessionRepository.GetOneAsync(request.SessionId);
        if (session is null) throw new Exception("No session");
        if (!session.IsActive) throw new Exception("Sessions finished");

        var answers = Map.ToEntity(request);
        session.SessionAnswers.Add(answers);
        await SessionRepository.UpdateAsync(session);
        
        await SendOkAsync(cancellation: cancellationToken);
    }
}