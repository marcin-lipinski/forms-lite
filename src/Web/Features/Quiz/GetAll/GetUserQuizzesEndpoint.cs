using Core.DataAccess;
using FastEndpoints;

namespace Web.Features.Quiz.GetAll;

public class GetUserQuizzesEndpoint : EndpointWithoutRequest<GetUserQuizzesResponse>
{
    public IRepository<Core.Entities.Quiz> QuizRepository { get; set; }
    public override void Configure()
    {
        Post("/api/quiz/get");
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        
    }
}