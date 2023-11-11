using Core.Entities.Quiz;
using Core.Exceptions.Security;
using FastEndpoints;
using MongoDB.Bson;
using MongoDB.Driver;
using Services.Interfaces;

namespace Web.Handlers.QuizHandlers.Update;
public class UpdateQuizEndpoint : Endpoint<UpdateQuizRequest, UpdateQuizResponse, UpdateQuizMapper>
{
    public IDbContext DbContext { get; set; } = null!;
    public IFilesService FilesService { get; set; } = null!;
    public IUserAccessor UserAccessor { get; set; } = null!;

    public override void Configure()
    {
        Post("/api/quiz/update/{QuizId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateQuizRequest request, CancellationToken cancellationToken)
    {
        var userId = UserAccessor.GetUserId();
        //if (string.IsNullOrEmpty(userId)) throw new UnauthorizedException();
        //if (await IsQuizTitleTaken(request.Quiz.Title, userId)) throw new QuizTitleTakenException();

        var originalQuiz = await DbContext.Collection<Quiz>()
            .Find(q => q.Id == request.QuizId)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);
        
        var quiz = Map.ToEntity(request);
        quiz.Title = originalQuiz.Title.Equals(request.Quiz.Title)
            ? originalQuiz.Title + " copy"
            : request.Quiz.Title;
        quiz.Version = request.ReplacePreviousVersion ? originalQuiz.Version + 1 : 0;
        foreach (var question in quiz.Questions)
        {
            var outImage = request.Quiz.Questions.SingleOrDefault(q => q.QuestionNumber == question.QuestionNumber)?.Image;
            if (outImage is not null)
            {
                question.Image = await FilesService.SaveImage(quiz.Title, question.QuestionNumber, outImage);
            }
        }

        if (request.ReplacePreviousVersion)
        {
            await DbContext.Collection<Quiz>().ReplaceOneAsync(q => q.Id == quiz.Id, quiz, cancellationToken: cancellationToken);
            await SendAsync(new UpdateQuizResponse{QuizId = request.QuizId}, cancellation: cancellationToken);
        }
        else
        {
            quiz.Id = ObjectId.GenerateNewId().ToString();
            await DbContext.Collection<Quiz>().InsertOneAsync(quiz, cancellationToken: cancellationToken);
            await SendAsync(new UpdateQuizResponse{QuizId = quiz.Id}, cancellation: cancellationToken);
        }
    }
}