using Core.DataAccess;
using FastEndpoints;
using Infrastructure.Persistence;

namespace Web.Features.Quiz.Create;
public class CreateQuizEndpoint : Endpoint<CreateQuizRequest, CreateQuizResponse, CreateQuizMapper>
{
    public IRepository<Core.Entities.Quiz> QuizRepository { get; set; } = null!;

    public override void Configure()
    {
        Post("/api/quiz/create");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateQuizRequest request, CancellationToken cancellationToken)
    {
        var quiz = Map.ToEntity(request);
        await QuizRepository.CreateAsync(quiz);
        //return SendAsync();
    }
}