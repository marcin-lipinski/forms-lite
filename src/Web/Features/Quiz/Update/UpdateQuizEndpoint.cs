using Core.DataAccess;
using Core.Exceptions.Quiz;
using Core.Exceptions.Security;
using FastEndpoints;
using Services.Security;
using Web.Features.Quiz.Create;

namespace Web.Features.Quiz.Update;
public class UpdateQuizEndpoint : Endpoint<UpdateQuizRequest, UpdateQuizResponse, UpdateQuizMapper>
{
    public IRepository<Core.Entities.Quiz> QuizRepository { get; set; } = null!;
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
        if (string.IsNullOrEmpty(userId)) throw new UnauthorizedException();
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

        if (request.ReplacePreviousVersion)
        {
            await QuizRepository.UpdateAsync(quiz);
            await SendAsync(new UpdateQuizResponse{QuizId = request.QuizId}, cancellation: cancellationToken);
        }
        
        var quizId = await QuizRepository.CreateAsync(quiz);
        await SendAsync(new UpdateQuizResponse{QuizId = quizId}, cancellation: cancellationToken);
    }
}