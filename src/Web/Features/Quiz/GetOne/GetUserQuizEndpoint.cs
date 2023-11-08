using FastEndpoints;

public class GetUserQuizEndpoint : EndpointWithoutRequest<GetUserQuizResponse>
{
    public override void Configure()
    {
        Post("/api/quiz/create");
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        
    }
}