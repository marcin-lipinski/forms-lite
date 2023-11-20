using Core.Entities.Question;
using Core.Entities.Quiz;

namespace Web.Handlers.QuizHandlers.Update;

public static class UpdateQuizMapper
{
    public static Quiz Map(this UpdateQuizRequest request)
    {
        return new Quiz
        {
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