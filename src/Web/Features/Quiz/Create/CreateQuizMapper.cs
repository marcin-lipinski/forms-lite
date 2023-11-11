using Core.Entities;
using FastEndpoints;
using Services.Security;

namespace Web.Features.Quiz.Create;

public class CreateQuizMapper : Mapper<CreateQuizRequest, CreateQuizResponse, Core.Entities.Quiz>
{
    public override Core.Entities.Quiz ToEntity(CreateQuizRequest request)
    {
        var userAccessor = Resolve<IUserAccessor>();
        return new Core.Entities.Quiz
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