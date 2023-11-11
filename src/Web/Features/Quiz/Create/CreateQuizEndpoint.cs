using Core.DataAccess;
using Core.Exceptions.Quiz;
using Core.Exceptions.Security;
using FastEndpoints;
using Services.Security;

namespace Web.Features.Quiz.Create;
public class CreateQuizEndpoint : Endpoint<CreateQuizRequest, CreateQuizResponse, CreateQuizMapper>
{
    public IRepository<Core.Entities.Quiz> QuizRepository { get; set; } = null!;
    public IFilesService FilesService { get; set; } = null!;
    public IUserAccessor UserAccessor { get; set; } = null!;

    public override void Configure()
    {
        Post("/api/quiz/create");
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
        
        var quizId = await QuizRepository.CreateAsync(quiz);
        await SendAsync(new CreateQuizResponse{QuizId = quizId}, cancellation: cancellationToken);
    }

    private async Task<bool> IsQuizTitleTaken(string quizTitle, string userId) =>
        (await QuizRepository.GetAllAsync()).Any(quiz => quiz.Title.Equals(quizTitle) && quiz.AuthorId.Equals(userId));
}