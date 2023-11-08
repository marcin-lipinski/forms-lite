using FastEndpoints;
using Server.Application.Services.Quiz.Create;
using Server.Application.Services.Quiz.GetAll;

public class GetUserQuizzesEndpoint : EndpointWithoutRequest<GetUserQuizzesResponse>
{
    public override void Configure()
    {
        Post("/api/quiz/create");
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        
    }
}