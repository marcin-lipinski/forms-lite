using System.Diagnostics;
using System.Text.Json;
using Core.Entities.Question;
using Core.Entities.Quiz;
using FastEndpoints;

namespace Web.Handlers.QuizHandlers.Create;

public class CreateQuizMapper : Mapper<CreateQuizRequest, CreateQuizResponse, Quiz>
{
    public override Quiz ToEntity(CreateQuizRequest request)
    {
        //var userAccessor = Resolve<IUserAccessor>();
        return new Quiz
        {
            AuthorId = "om",//userAccessor.GetUserId(),
            Title = request.Quiz.Title,
            Version = 0,
            Questions = request.Quiz.Questions.Select(question => question.QuestionType == QuestionType.Closed 
                ? new Question
                {
                    ContentText = question.ContentText,
                    QuestionNumber = question.QuestionNumber,
                    QuestionType = QuestionType.Closed
                }
                : (Question)new Question
                {
                    ContentText = question.ContentText,
                    QuestionNumber = question.QuestionNumber,
                    QuestionType = QuestionType.Open,
                    Answers = question.Answers != null ? question.Answers.ToList() : new List<string>(),
                    CorrectAnswer = question.CorrectAnswer
                }).ToList()
        };
    }
}