using Core.Entities.Quiz;
using FastEndpoints;
using MongoDB.Driver;
using Services.Interfaces;

namespace Web.Handlers.QuizHandlers.GetAll;

public class GetUserQuizzesEndpoint : EndpointWithoutRequest<GetUserQuizzesResponse, GetUserQuizzesMapper>
{
    public IDbContext DbContext { get; set; } = null!;
    public IUserAccessor UserAccessor { get; set; } = null!;

    public override void Configure()
    {
        Get("/api/quiz/get");
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var userId = UserAccessor.GetUserId();
        var quizzes = DbContext.Collection<Quiz>().AsQueryable()
            .Where(q => q.AuthorId == userId).ToList();

        var response = Map.FromEntity(quizzes);
        await SendAsync(response, cancellation: cancellationToken);
    }
}