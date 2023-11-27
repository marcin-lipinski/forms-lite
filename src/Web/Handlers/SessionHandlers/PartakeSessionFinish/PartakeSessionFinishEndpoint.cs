using Core.Entities.Question;
using Core.Entities.Quiz;
using Core.Entities.Session;
using Core.Exceptions.Quiz;
using Core.Exceptions.Session;
using FastEndpoints;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Services.Interfaces;

namespace Web.Handlers.SessionHandlers.PartakeSessionFinish;

public class PartakeSessionFinishEndpoint : Endpoint<PartakeSessionFinishRequest, PartakeSessionFinishResponse, PartakeSessionFinishMapper>
{
    public IDbContext DbContext { get; set; } = null!;

    public override void Configure()
    {
        Post("/api/session/partake/finish/{SessionId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(PartakeSessionFinishRequest request, CancellationToken cancellationToken)
    {
        var session = await DbContext.Collection<Session>().AsQueryable()
            .SingleOrDefaultAsync(s => s.Id.Equals(request.SessionId), cancellationToken: cancellationToken);
        if (session is null) throw new NotFoundException("Session");
        if (session.IsFinishedByAuthor && DateTime.Now > session.StartTime.ToLocalTime() && DateTime.Now < session.FinishTime.ToLocalTime()) throw new SessionNotActiveException();

        var answers = Map.ToEntity(request);
        session.SessionAnswers.Add(answers);
        await DbContext.Collection<Session>().ReplaceOneAsync(s => s.Id.Equals(session.Id), session, cancellationToken: cancellationToken);
        session = await DbContext.Collection<Session>().AsQueryable().SingleOrDefaultAsync(s => s.Id == request.SessionId, cancellationToken: cancellationToken);
        var quiz = await DbContext.Collection<Quiz>().AsQueryable().SingleOrDefaultAsync(s => s.Id == session.QuizId, cancellationToken: cancellationToken);

        var results = quiz.Questions
            .Where(question => question.QuestionType == QuestionType.Closed)
            .Select(question => new PartakeSessionSingleResult
            {
                QuestionContent = question.ContentText,
                Answers = question.Answers!.Select(answer => new PartakeSessionSingleResultSingle
                {
                    Text = answer,
                    Value = Math.Round((double)session.SessionAnswers
                                    .Count(sa => sa.Answers
                                            .FirstOrDefault(p => p.Id == question.Id)?.QuestionAnswer
                                            == answer)
                                        / session.SessionAnswers.Count, 
                                        2)
                }).ToList(),
                ParticipantAnswer =  request.Answers.First(r => r.Id == question.Id).QuestionAnswer
            }).ToList();

        var response = new PartakeSessionFinishResponse
        {
            QuizTitle = quiz.Title,
            Results = results
        };
        
        await SendAsync(response, cancellation: cancellationToken);
    }
}