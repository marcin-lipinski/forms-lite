using Core.Entities.Quiz;
using Core.Entities.Session;
using Core.Exceptions.Quiz;
using FastEndpoints;
using MongoDB.Bson;
using MongoDB.Driver;
using Services.Interfaces;

namespace Web.Handlers.SessionHandlers.Create;
public class CreateSessionEndpoint : Endpoint<CreateSessionRequest, CreateSessionResponse, CreateSessionMapper>
{
    public IDbContext DbContext { get; set; } = null!;
    public IUserAccessor UserAccessor { get; set; } = null!;

    public override void Configure()
    {
        Put("/api/session/create/{QuizId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateSessionRequest request, CancellationToken cancellationToken)
    {
        var userId = UserAccessor.GetUserId();
        
        var quiz = await DbContext.Collection<Quiz>()
            .Find(q => q.Id == request.QuizId && q.AuthorId == userId)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);
        
        if (quiz is null) throw new NotFoundException("Quiz");

        var sessionId = ObjectId.GenerateNewId().ToString();
        var session = new Session
        {
            Id = sessionId,
            StartTime = DateTime.ParseExact(request.StartTime, "dd-MM-yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture),
            FinishTime = DateTime.ParseExact(request.FinishTime, "dd-MM-yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture),
            QuizId = request.QuizId,
            PartakeUrl = "/partake/" + sessionId,
            SessionAnswers = new List<SessionPartake>()
        };
        
        await DbContext.Collection<Session>().InsertOneAsync(session, cancellationToken: cancellationToken);
        await SendAsync(new CreateSessionResponse{SessionPartakeUrl = HttpContext.Request.Headers.Origin[0] + session.PartakeUrl}, cancellation: cancellationToken);
    }
}