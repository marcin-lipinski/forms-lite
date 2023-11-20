using Core.Entities.Question;
using Core.Entities.Quiz;

namespace Web.Handlers.QuizHandlers.Create;

public static class CreateQuizMapper
{
    public static Quiz Map(this CreateQuizRequest request)
    {
        return new Quiz
        {
            Title = request.Quiz.Title,
            Questions = request.Quiz.Questions.Select(question => question.QuestionType == QuestionType.Open 
                ? new Question
                {
                    Id = question.Id,
                    ContentText = question.ContentText,
                    QuestionType = QuestionType.Open
                }
                : new Question
                {
                    Id = question.Id,
                    ContentText = question.ContentText,
                    QuestionType = QuestionType.Closed,
                    Answers = question.Answers != null ? question.Answers.ToList() : new List<string>(),
                    CorrectAnswer = question.CorrectAnswer
                }).ToList()
        };
    }
}