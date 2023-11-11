using Core.Entities.Quiz;
using Core.Entities.Session;
using FastEndpoints;
using Services.Interfaces;

namespace Web.Handlers.SessionHandlers.GetAll;

public class GetUserSessionsMapper : ResponseMapper<GetUserSessionsResponse, List<Session>>
{
    public override GetUserSessionsResponse FromEntity(List<Session> sessions)
    {
        var quizRepository = Resolve<IDbContext>();
        return new GetUserSessionsResponse
        {
            Sessions = sessions.Select(session => new SessionDto
            {
                AnswersAmount = session.SessionAnswers.Count,
                FinishTime = session.FinishTime,
                StartTime = session.StartTime,
                Id = session.Id,
                IsActive = session.IsActive
            }).ToList()
        };
    }
}