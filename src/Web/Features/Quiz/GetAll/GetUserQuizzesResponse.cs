using Core.Entities;

namespace Web.Features.Quiz.GetAll;

public class GetUserQuizzesResponse
{
    public List<QuizDto> Quizzes { get; init; }
}

public class QuizDto
{
    public string Id { get; init; }
    public List<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
}

public class QuestionDto
{
    public string ContentImageUrl { get; set; } = null!;
    public string ContentText { get; set; } = null!;
    public QuestionType QuestionType { get; set; }
    public string QuestionNumber { get; set; } = null!;
    public List<string>? Answers { get; set; }
    public int? CorrectAnswer { get; set; }
}