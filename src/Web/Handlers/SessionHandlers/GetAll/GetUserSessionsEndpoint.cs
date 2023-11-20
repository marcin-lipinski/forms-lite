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
        Get("/api/session/get");
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var userId = UserAccessor.GetUserId();

        var userQuizzesIds = await DbContext.Collection<Quiz>().AsQueryable()
            .Where(q => q.AuthorId.Equals(userId))
            .Select(quiz => new {quiz.Id, quiz.Title}).ToListAsync(cancellationToken: cancellationToken);

        var sessions = await DbContext.Collection<Session>().AsQueryable()
            .Where(session => userQuizzesIds.Any(q => q.Id.Equals(session.QuizId)))
            .ToListAsync(cancellationToken: cancellationToken);

        var result = sessions.Select(session => new SessionDto
        {
            Id = session.Id,
            Answers = session.SessionAnswers,
            FinishTime = session.FinishTime.ToLocalTime().ToString("dd-MM-yyyy HH:mm"),
            StartTime = session.StartTime.ToLocalTime().ToString("dd-MM-yyyy HH:mm"),
            JoinUrl = HttpContext.Request.Headers.Origin[0] + session.PartakeUrl,
            QuizId = session.QuizId,
            IsActive = !session.IsFinishedByAuthor && DateTime.Now > session.StartTime.ToLocalTime() && DateTime.Now < session.FinishTime.ToLocalTime(),
            QuizTitle = userQuizzesIds.SingleOrDefault(q => q.Id.Equals(session.QuizId))!.Title,
            Questions = DbContext.Collection<Quiz>().AsQueryable().SingleOrDefault(q => q.Id == session.QuizId)!.Questions.Select(q => new QuestionDto
            {
                Id = q.Id,
                ContentText = q.ContentText
            }).ToList()
        }).ToList();

        await SendAsync(new GetUserSessionsResponse{Sessions = result}, cancellation: cancellationToken);
    }
}