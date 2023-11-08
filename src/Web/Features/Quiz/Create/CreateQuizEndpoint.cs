using FastEndpoints;
using Infrastructure.Persistence;

namespace Web.Features.Quiz.Create;
public class CreateQuizEndpoint : Endpoint<CreateQuizRequest, CreateQuizResponse, CreateQuizMapper>
{
    public IMongoService<Core.Entities.Quiz> _quizService { get; set; }

    public override void Configure()
    {
        Post("/api/quiz/create");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateQuizRequest request, CancellationToken cancellationToken)
    {
        var quiz = Map.ToEntity(request);
        await _quizService.CreateAsync(quiz);
        //return SendAsync();
    }
}