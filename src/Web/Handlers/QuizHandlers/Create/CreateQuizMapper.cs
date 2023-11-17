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