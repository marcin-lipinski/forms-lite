using Core.DataAccess;
using FastEndpoints;
using Services.Security;

namespace Web.Features.Quiz.GetOne;

public class GetUserQuizEndpoint  : Endpoint<GetUserQuizRequest, GetUserQuizResponse, GetUserQuizMapper>
{
    public IRepository<Core.Entities.Quiz> QuizRepository { get; set; } = null!;
    public IUserAccessor UserAccessor { get; set; } = null!;

    [EndpointName("GetQuiz")]
    public override void Configure()
    {
        Get("/api/quiz/get/{QuizId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetUserQuizRequest request, CancellationToken cancellationToken)
    {
        var userId = UserAccessor.GetUserId();
        var quiz = await QuizRepository.GetOneAsync(request.QuizId);
        if (quiz is null) throw new NotImplementedException();
        if (!quiz.AuthorId.Equals(userId)) throw new Exception();
        
        var response = Map.FromEntity(quiz);
        await SendAsync(response, cancellation: cancellationToken);
    }
}