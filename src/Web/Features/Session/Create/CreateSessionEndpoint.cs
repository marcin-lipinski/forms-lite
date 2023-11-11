using Core.DataAccess;
using Core.Exceptions.Security;
using FastEndpoints;
using Services.Security;
using Web.Features.Quiz.Create;

namespace Web.Features.Session.Create;
public class CreateSessionEndpoint : Endpoint<CreateSessionRequest, CreateSessionResponse, CreateSessionMapper>
{
    public IRepository<Core.Entities.Quiz> QuizRepository { get; set; } = null!;
    public IRepository<Core.Entities.Session> SessionRepository { get; set; } = null!;
    public IUserAccessor UserAccessor { get; set; } = null!;
    public IHttpContextAccessor HttpContextAccessor { get; set; } = null!;

    public override void Configure()
    {
        Post("/api/session/create/{QuizId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateSessionRequest request, CancellationToken cancellationToken)
    {
        //var userId = UserAccessor.GetUserId();
        //if (string.IsNullOrEmpty(userId)) throw new UnauthorizedException();
        
        //does quiz exists
        var quiz = await QuizRepository.GetOneAsync(request.QuizId);
        
        var quizId = await QuizRepository.CreateAsync(quiz);
        if (quiz is null) throw new Exception("Quiz with provided id does not exists");

        var session = new Core.Entities.Session
        {
            StartTime = request.StartTime,
            FinishTime = request.FinishTime,
            QuizId = request.QuizId
        };
        var sessionId = await SessionRepository.CreateAsync(session);
        
        var scheme = HttpContextAccessor.HttpContext!.Request.Scheme;
        var host = HttpContextAccessor.HttpContext.Request.Host;
        var pathBase = HttpContextAccessor.HttpContext.Request.PathBase;
        var sessionPartakeUrl = string.Concat(scheme, "://", host, pathBase, "/api/session/partake/", sessionId);
        
        await SendAsync(new CreateSessionResponse{SessionPartakeUrl = sessionPartakeUrl}, cancellation: cancellationToken);
    }
}