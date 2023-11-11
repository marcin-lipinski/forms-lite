using Core.Entities.Quiz;
using Core.Entities.Session;
using FastEndpoints;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Services.Interfaces;

namespace Web.Handlers.SessionHandlers.GetAll;

public class GetUserSessionsEndpoint : EndpointWithoutRequest<GetUserSessionsResponse, GetUserSessionsMapper>
{
    public IDbContext DbContext { get; set; } = null!;
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

        var userQuizzesIds = DbContext.Collection<Quiz>().AsQueryable()
            .Where(q => q.AuthorId.Equals(userId))
            .Select(quiz => new {quiz.Id, quiz.Title});

        var sessions = await DbContext.Collection<Session>().AsQueryable()
            .Where(session => userQuizzesIds.Any(q => q.Id.Equals(session.QuizId)))
            .Select(session => new SessionDto
            {
                AnswersAmount = session.SessionAnswers.Count,
                FinishTime = session.FinishTime,
                StartTime = session.StartTime,
                Id = session.Id,
                IsActive = session.IsActive,
                QuizTitle = userQuizzesIds.SingleOrDefault(q => q.Equals(session.QuizId))!.Title
            })
            .ToListAsync(cancellationToken: cancellationToken);

        await SendAsync(new GetUserSessionsResponse{Sessions = sessions}, cancellation: cancellationToken);
    }
}