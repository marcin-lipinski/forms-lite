using Core.Entities.Question;
using FastEndpoints;
using Services.Interfaces;

namespace Web.Handlers.QuizHandlers.GetAll;

public class GetUserQuizzesMapper : ResponseMapper<GetUserQuizzesResponse, List<Core.Entities.Quiz.Quiz>>
{
    public override GetUserQuizzesResponse FromEntity(List<Core.Entities.Quiz.Quiz> entities)
    {
        var filesService = Resolve<IFilesService>();
        return new GetUserQuizzesResponse
        {
            Quizzes = entities.Select(entity => new QuizDto
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Questions = entity.Questions.Select(question => new QuestionDto
                    {
                        ContentText = question.ContentText,
                        ContentImageUrl = question.Image is not null ? filesService.CreateImageUrl(question.Image.RelativePath) : null,
                        QuestionNumber = question.QuestionNumber,
                        QuestionType = question.QuestionType,
                        Answers = question.QuestionType == QuestionType.Closed 
                            ? question.Answers 
                            : null,
                        CorrectAnswer = question.QuestionType == QuestionType.Closed
                            ? question.CorrectAnswer
                            : null
                    }).ToList()
                }).ToList()
        };
    }
}