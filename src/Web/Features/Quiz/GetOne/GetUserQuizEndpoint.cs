using Core.DataAccess;
using FastEndpoints;

namespace Web.Features.Quiz.GetOne;

public class GetUserQuizEndpoint : EndpointWithoutRequest<GetUserQuizResponse>
{
    public IRepository<Core.Entities.Quiz> QuizRepository { get; set; }
    public override void Configure()
    {
        Post("/api/quiz/get/{id}");
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        
    }
}