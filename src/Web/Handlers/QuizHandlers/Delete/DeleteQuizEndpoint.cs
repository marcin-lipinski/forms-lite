using Core.Entities.Quiz;
using Core.Exceptions.Quiz;
using FastEndpoints;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Services.Interfaces;

namespace Web.Handlers.QuizHandlers.Delete;

public class DeleteQuizEndpoint : Endpoint<DeleteQuizRequest, EmptyResponse> 
{
    public IDbContext DbContext { get; set; } = null!;

    public override void Configure()
    {
        Delete("/api/quiz/delete/{QuizId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteQuizRequest request, CancellationToken cancellationToken)
    {
        //var userId = UserAccessor.GetUserId();
        //if (string.IsNullOrEmpty(userId)) throw new UnauthorizedException();
        //if (await IsQuizTitleTaken(request.Quiz.Title, userId)) throw new QuizTitleTakenException();
        
        var quiz = await DbContext.Collection<Quiz>().AsQueryable().SingleOrDefaultAsync(q => q.Id.Equals(request.QuizId), cancellationToken: cancellationToken);
        if(quiz is null) throw new NotFoundException("Quiz");

        foreach (var question in quiz.Questions)
        {
            if (question.Image != null) Directory.Delete(question.Image.FullPath, true);
        }

        await DbContext.Collection<Quiz>().DeleteOneAsync(q => q.Id.Equals(request.QuizId), cancellationToken: cancellationToken);

        await SendOkAsync(cancellation: cancellationToken);
    }
}