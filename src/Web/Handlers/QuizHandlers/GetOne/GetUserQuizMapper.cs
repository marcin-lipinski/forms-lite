using Core.Entities.Question;
using FastEndpoints;
using Services.Interfaces;
using Web.Handlers.QuizHandlers.GetAll;

namespace Web.Handlers.QuizHandlers.GetOne;

public class GetUserQuizMapper : ResponseMapper<GetUserQuizResponse, Core.Entities.Quiz.Quiz>
{
    public override GetUserQuizResponse FromEntity(Core.Entities.Quiz.Quiz entity)
    {
        var filesService = Resolve<IFilesService>();
        return new GetUserQuizResponse
        {
            Quiz = new QuizDto 
            {
                Id = entity.Id,
                Title = entity.Title,
                Questions = entity.Questions.Select(question => new QuestionDto
                {
                    Id = question.Id,
                    ContentText = question.ContentText,
                    ContentImageUrl = question.Image is not null ? filesService.CreateImageUrl(question.Image.RelativePath) : null,
                    QuestionType = question.QuestionType,
                    Answers = question.QuestionType == QuestionType.Open
                        ? question.Answers
                        : null,
                    CorrectAnswer = question.QuestionType == QuestionType.Open
                        ? question.CorrectAnswer
                        : null
                }).ToList()
            }
        };
    }
}