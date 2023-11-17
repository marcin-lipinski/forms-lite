using Core.Entities.Quiz;
using Core.Exceptions.Quiz;
using FastEndpoints;
using MongoDB.Driver;
using Services.Interfaces;

namespace Web.Handlers.QuizHandlers.GetOne;

public class GetUserQuizEndpoint  : Endpoint<GetUserQuizRequest, GetUserQuizResponse, GetUserQuizMapper>
{
    public IDbContext DbContext { get; set; } = null!;
    public IUserAccessor UserAccessor { get; set; } = null!;
    
    public override void Configure()
    {
        Get("/api/quiz/get/{QuizId}");
    }

    public override async Task HandleAsync(GetUserQuizRequest request, CancellationToken cancellationToken)
    {
        var userId = UserAccessor.GetUserId();
        var quiz = DbContext.Collection<Quiz>().AsQueryable()
            .SingleOrDefault(q => q.Id == request.QuizId && q.AuthorId == userId);
        
        if (quiz is null) throw new NotFoundException("Quiz");
        
        var response = Map.FromEntity(quiz);
        await SendAsync(response, cancellation: cancellationToken);
    }
}