using Core.DataAccess;
using Core.Entities;
using FastEndpoints;
using Services.Security;

namespace Web.Features.Quiz.Update;

public class UpdateQuizMapper : Mapper<UpdateQuizRequest, UpdateQuizResponse, Core.Entities.Quiz>
{
    public override Core.Entities.Quiz ToEntity(UpdateQuizRequest request)
    {
        var quizRepository = Resolve<IRepository<Core.Entities.Quiz>>();
        var userAccessor = Resolve<IUserAccessor>();
        return new Core.Entities.Quiz
        {
            Id = request.QuizId,
            AuthorId = userAccessor.GetUserId(),
            Title = quizRepository.GetOneAsync(request.QuizId).Result.Title.Equals(request.Quiz.Title) 
                ? quizRepository.GetOneAsync(request.QuizId).Result.Title + " v." + (quizRepository.GetOneAsync(request.QuizId).Result.Version + 1)
                : request.Quiz.Title,
            Version = request.ReplacePreviousVersion ? quizRepository.GetOneAsync(request.QuizId).Result.Version + 1 : 0,
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