using Core.Entities.Quiz;
using FastEndpoints;
using MongoDB.Bson;
using MongoDB.Driver;
using Services.Interfaces;

namespace Web.Handlers.QuizHandlers.Create;
public class CreateQuizEndpoint : Endpoint<CreateQuizRequest, CreateQuizResponse, CreateQuizMapper>
{
    public IDbContext DbContext { get; set; } = null!;
    public IFilesService FilesService { get; set; } = null!;
    public IUserAccessor UserAccessor { get; set; } = null!;

    public override void Configure()
    {
        Put("/api/quiz/create");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateQuizRequest request, CancellationToken cancellationToken)
    {
        //var userId = UserAccessor.GetUserId();
        //if (string.IsNullOrEmpty(userId)) throw new UnauthorizedException();
        //if (await IsQuizTitleTaken(request.Quiz.Title, userId)) throw new QuizTitleTakenException();
        
        var quiz = Map.ToEntity(request);
        foreach (var question in quiz.Questions)
        {
            var outImage = request.Quiz.Questions.SingleOrDefault(q => q.QuestionNumber == question.QuestionNumber)?.Image;
            if (outImage is not null)
            {
                question.Image = await FilesService.SaveImage(quiz.Title, question.QuestionNumber, outImage);
            }
        }

        quiz.Id = ObjectId.GenerateNewId().ToString();
        await DbContext.Collection<Quiz>().InsertOneAsync(quiz, cancellationToken: cancellationToken);
        await SendAsync(new CreateQuizResponse{QuizId = quiz.Id}, cancellation: cancellationToken);
    }

    private async Task<bool> IsQuizTitleTaken(string quizTitle, string userId) =>
        await DbContext.Collection<Quiz>().Find(q => q.Title == quizTitle && q.AuthorId == userId).CountDocumentsAsync() > 0;
}