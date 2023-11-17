using Core.Entities.Question;
using Core.Entities.Quiz;
using FastEndpoints;
using Services.Interfaces;

namespace Web.Handlers.QuizHandlers.Update;

public static class UpdateQuizMapper
{
    public static Quiz Map(this UpdateQuizRequest request)
    {
        return new Quiz
        {
            Id = request.QuizId,
            Questions = request.Quiz.Questions.Select(question => question.QuestionType == QuestionType.Closed 
                ? new Question
                {
                    QuestionType = QuestionType.Closed
                }
                : new Question
                {
                    QuestionType = QuestionType.Open,
                    Answers = question.Answers,
                    CorrectAnswer = question.CorrectAnswer
                }).ToList()
        };
    }
}