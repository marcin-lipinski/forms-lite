using Core.Entities.Quiz;
using Core.Entities.Session;
using Core.Exceptions.Quiz;
using Core.Exceptions.Security;
using FastEndpoints;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Services.Interfaces;

namespace Web.Handlers.SessionHandlers.Delete;

public class SessionDeleteHandler : Endpoint<SessionDeleteRequest, EmptyResponse>
{
    public IDbContext DbContext { get; set; } = null!;
    public IUserAccessor UserAccessor { get; set; } = null!;
    
    public override void Configure()
    {
        Delete("/api/session/delete/{SessionId}");
    }

    public override async Task HandleAsync(SessionDeleteRequest request, CancellationToken cancellationToken)
    {
        var userId = UserAccessor.GetUserId();

        var session = await DbContext.Collection<Session>().AsQueryable()
            .Where(q => q.Id == request.SessionId).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        if (session is null) throw new NotFoundException("Session");

        var quiz = await DbContext.Collection<Quiz>().AsQueryable()
            .SingleOrDefaultAsync(quiz => quiz.AuthorId == userId && quiz.Id == session.QuizId, cancellationToken: cancellationToken);
        if (quiz is null) throw new UnauthorizedException();

        await DbContext.Collection<Session>().DeleteOneAsync(s => s.Id == request.SessionId, cancellationToken: cancellationToken);

        await SendOkAsync(cancellationToken);
    }
    
}