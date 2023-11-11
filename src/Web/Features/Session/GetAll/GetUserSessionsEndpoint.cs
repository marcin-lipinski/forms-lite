using Core.DataAccess;
using FastEndpoints;
using Services.Security;
using Web.Features.Quiz.Create;

namespace Web.Features.Session.GetAll;

public class GetUserSessionsEndpoint : EndpointWithoutRequest<GetUserSessionsResponse, GetUserSessionsMapper>
{
    public IRepository<Core.Entities.Quiz> QuizRepository { get; set; } = null!;
    public IRepository<Core.Entities.Session> SessionRepository { get; set; } = null!;
    public IUserAccessor UserAccessor { get; set; } = null!;

    public override void Configure()
    {
        Post("/api/session/get");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var userId = UserAccessor.GetUserId();
        //if (string.IsNullOrEmpty(userId)) throw new UnauthorizedException();
        //if (await IsQuizTitleTaken(request.Quiz.Title, userId)) throw new QuizTitleTakenException();

        var userQuizzesIds = (await QuizRepository.GetAllAsync())
            .Where(quiz => quiz.AuthorId.Equals(userId))
            .Select(quiz => quiz.Id).ToList();

        var sessions = (await SessionRepository.GetAllAsync())
            .Where(session => userQuizzesIds.Contains(session.QuizId))
            .ToList();

        var response = Map.FromEntity(sessions);
        
        await SendAsync(response, cancellation: cancellationToken);
    }
}