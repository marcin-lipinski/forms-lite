using Core.Entities.Question;
using Core.Entities.Quiz;
using FastEndpoints;
using Services.Interfaces;

namespace Web.Handlers.QuizHandlers.Update;

public class UpdateQuizMapper : Mapper<UpdateQuizRequest, UpdateQuizResponse, Quiz>
{
    public override Quiz ToEntity(UpdateQuizRequest request)
    {
        var userAccessor = Resolve<IUserAccessor>();
        return new Quiz
        {
            Id = request.QuizId,
            AuthorId = userAccessor.GetUserId(),
            Questions = request.Quiz.Questions.Select(question => question.QuestionType == QuestionType.Closed 
                ? new QuestionClosed
                {
                    QuestionType = QuestionType.Closed
                }
                : (Question)new QuestionOpen
                {
                    QuestionType = QuestionType.Open,
                    Answers = question.Answers,
                    CorrectAnswer = question.CorrectAnswer
                }).ToList()
        };
    }
}