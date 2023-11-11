using System.Text.Json;
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

        var userQuizzesIds = await DbContext.Collection<Quiz>().AsQueryable()
            //.Where(q => q.AuthorId.Equals(userId))
            .Select(quiz => new {quiz.Id, quiz.Title}).ToListAsync(cancellationToken: cancellationToken);

        var sessions = await DbContext.Collection<Session>().AsQueryable()
            .Where(session => userQuizzesIds.Any(q => q.Id.Equals(session.QuizId)))
            .Select(session => new SessionDto
            {
                AnswersAmount = session.SessionAnswers.Count,
                FinishTime = session.FinishTime,
                StartTime = session.StartTime,
                Id = session.Id,
                QuizId = session.QuizId,
                IsActive = !session.IsFinishedByAuthor && DateTime.Now > session.StartTime && DateTime.Now < session.FinishTime
            })
            .ToListAsync(cancellationToken: cancellationToken);

        sessions?.ForEach(session => session.QuizTitle = userQuizzesIds.SingleOrDefault(q => q.Id.Equals(session.QuizId))!.Title);

        await SendAsync(new GetUserSessionsResponse{Sessions = sessions}, cancellation: cancellationToken);
    }
}