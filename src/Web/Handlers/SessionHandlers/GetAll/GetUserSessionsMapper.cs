using Core.Entities.Session;
using FastEndpoints;

namespace Web.Handlers.SessionHandlers.GetAll;

public class GetUserSessionsMapper : ResponseMapper<GetUserSessionsResponse, List<Session>>
{
    public override GetUserSessionsResponse FromEntity(List<Session> sessions)
    {
        return new GetUserSessionsResponse
        {
            Sessions = sessions.Select(session => new SessionDto
            {
                AnswersAmount = session.SessionAnswers.Count,
                FinishTime = session.FinishTime.ToString("dd-MM-yyyy hh:mm"),
                StartTime = session.StartTime.ToString("dd-MM-yyyy hh:mm"),
                IsActive = !session.IsFinishedByAuthor && DateTime.Now > session.StartTime && DateTime.Now < session.FinishTime
            }).ToList()
        };
    }
}