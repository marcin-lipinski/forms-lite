using Core.Entities.Quiz;
using Core.Exceptions.Quiz;
using Core.Exceptions.Security;
using FastEndpoints;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Services.Interfaces;

namespace Web.Handlers.QuizHandlers.Delete;

public class DeleteQuizEndpoint : Endpoint<DeleteQuizRequest, EmptyResponse> 
{
    public IDbContext DbContext { get; set; } = null!;
    public IUserAccessor UserAccessor { get; set; } = null!;

    public override void Configure()
    {
        Delete("/api/quiz/delete/{QuizId}");
    }

    public override async Task HandleAsync(DeleteQuizRequest request, CancellationToken cancellationToken)
    {
        var userId = UserAccessor.GetUserId();
        var quiz = await DbContext.Collection<Quiz>().AsQueryable()
            .SingleOrDefaultAsync(q => q.Id == request.QuizId && q.AuthorId == userId, cancellationToken: cancellationToken);
        if(quiz is null) throw new NotFoundException("Quiz");

        foreach (var question in quiz.Questions)
        {
            if (question.Image == null) continue;
            Directory.Delete(Path.GetDirectoryName(question.Image.FullPath), true);
            break;
        }

        await DbContext.Collection<Quiz>().DeleteOneAsync(q => q.Id.Equals(request.QuizId), cancellationToken: cancellationToken);
        await SendOkAsync(cancellation: cancellationToken);
    }
}