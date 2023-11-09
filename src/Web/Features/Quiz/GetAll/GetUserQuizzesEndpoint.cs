using Core.DataAccess;
using FastEndpoints;
using Services.Security;

namespace Web.Features.Quiz.GetAll;

public class GetUserQuizzesEndpoint : EndpointWithoutRequest<GetUserQuizzesResponse, GetUserQuizzesMapper>
{
    public IRepository<Core.Entities.Quiz> QuizRepository { get; set; } = null!;
    public IUserAccessor UserAccessor { get; set; } = null!;

    public override void Configure()
    {
        Get("/api/quiz/get");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var userId = UserAccessor.GetUserId();
        var quizzes = await QuizRepository.GetAllAsync();
        quizzes = quizzes.Where(quiz => quiz.AuthorId.Equals(userId)).ToList();
        
        var response = Map.FromEntity(quizzes);
        await SendAsync(response, cancellation: cancellationToken);
    }
}