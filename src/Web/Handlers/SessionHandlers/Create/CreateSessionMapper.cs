using Core.Entities.Question;
using FastEndpoints;
using Services.Interfaces;
using Web.Handlers.QuizHandlers.Create;

namespace Web.Handlers.SessionHandlers.Create;

public class CreateSessionMapper : Mapper<CreateQuizRequest, CreateQuizResponse, Core.Entities.Quiz.Quiz>
{
    public override Core.Entities.Quiz.Quiz ToEntity(CreateQuizRequest request)
    {
        var userAccessor = Resolve<IUserAccessor>();
        return new Core.Entities.Quiz.Quiz
        {
            AuthorId = userAccessor.GetUserId(),
            Title = request.Quiz.Title,
            Version = 0,
            Questions = request.Quiz.Questions.Select(question => question.QuestionType == QuestionType.Closed 
                ? (Question)new QuestionClosed
                {
                    ContentText = question.ContentText,
                    QuestionNumber = question.QuestionNumber,
                    QuestionType = QuestionType.Closed
                }
                : (Question)new QuestionOpen
                {
                    ContentText = question.ContentText,
                    QuestionNumber = question.QuestionNumber,
                    QuestionType = QuestionType.Open,
                    Answers = question.Answers,
                    CorrectAnswer = question.CorrectAnswer
                }).ToList()
        };
    }
}