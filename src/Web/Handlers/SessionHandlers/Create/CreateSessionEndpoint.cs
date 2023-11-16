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
    public IHttpContextAccessor HttpContextAccessor { get; set; } = null!;

    public override void Configure()
    {
        Put("/api/session/create/{QuizId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateSessionRequest request, CancellationToken cancellationToken)
    {
        //var userId = UserAccessor.GetUserId();
        //if (string.IsNullOrEmpty(userId)) throw new UnauthorizedException();
        
        //does quiz exists
        var quiz = await DbContext.Collection<Quiz>()
            .Find(q => q.Id == request.QuizId)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);
        
        if (quiz is null) throw new NotFoundException("Quiz");

        var scheme = HttpContextAccessor.HttpContext!.Request.Scheme;
        var host = HttpContextAccessor.HttpContext.Request.Host;
        var pathBase = HttpContextAccessor.HttpContext.Request.PathBase;

        var sessionId = ObjectId.GenerateNewId().ToString();
        var session = new Session
        {
            Id = sessionId,
            StartTime = DateTime.Parse(request.StartTime),
            FinishTime = DateTime.Parse(request.FinishTime),
            QuizId = request.QuizId,
            PartakeUrl = string.Concat(scheme, "://", host, pathBase, "/api/session/partake/", sessionId),
            SessionAnswers = new List<SessionPartake>()
        };
        await DbContext.Collection<Session>().InsertOneAsync(session, cancellationToken: cancellationToken);

        await SendAsync(new CreateSessionResponse{SessionPartakeUrl = session.PartakeUrl}, cancellation: cancellationToken);
    }
}