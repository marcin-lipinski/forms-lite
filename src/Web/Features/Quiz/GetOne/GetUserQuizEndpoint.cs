using FastEndpoints;

namespace Web.Features.Quiz.GetOne;

public class GetUserQuizEndpoint : EndpointWithoutRequest<GetUserQuizResponse>
{
    public override void Configure()
    {
        Post("/api/quiz/get/{id}");
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        
    }
}