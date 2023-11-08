using FastEndpoints;
using Server.Application.Services;
using Server.Application.Services.Quiz.Create;
using Server.Domain.Entities;

public class CreateQuizEndpoint : Endpoint<CreateQuizRequest, CreateQuizResponse, CreateQuizMapper>
{
    public IService<Quiz> _quizService { get; set; }

    public override void Configure()
    {
        Post("/api/quiz/create");
    }

    public override async Task HandleAsync(CreateQuizRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine(request.Info);
        var quiz = Map.ToEntity(request);
        await _quizService.CreateAsync(quiz);
        //return SendAsync();
    }
}