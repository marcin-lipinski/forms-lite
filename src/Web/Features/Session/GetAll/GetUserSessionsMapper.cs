using Core.DataAccess;
using FastEndpoints;
using Web.Features.Quiz.Create;

namespace Web.Features.Session.GetAll;

public class GetUserSessionsMapper : ResponseMapper<GetUserSessionsResponse, List<Core.Entities.Session>>
{
    public override GetUserSessionsResponse FromEntity(List<Core.Entities.Session> sessions)
    {
        var quizRepository = Resolve<IRepository<Core.Entities.Quiz>>();
        return new GetUserSessionsResponse
        {
            Sessions = sessions.Select(session => new SessionDto
            {
                AnswersAmount = session.SessionAnswers.Count,
                FinishTime = session.FinishTime,
                StartTime = session.StartTime,
                Id = session.Id,
                IsActive = session.IsActive,
                QuizTitle = quizRepository.GetOneAsync(session.QuizId).Result.Title
            }).ToList()
        };
    }
}