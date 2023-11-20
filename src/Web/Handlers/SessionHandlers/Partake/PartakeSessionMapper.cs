using Core.Entities.Question;
using FastEndpoints;
using Services.Interfaces;
using Web.Handlers.QuizHandlers.GetAll;

namespace Web.Handlers.SessionHandlers.Partake;

public class PartakeSessionMapper : Mapper<PartakeSessionRequest, PartakeSessionResponse, Core.Entities.Quiz.Quiz>
{
    public override PartakeSessionResponse FromEntity(Core.Entities.Quiz.Quiz entity)
    {
        var filesService = Resolve<IFilesService>();
        return new PartakeSessionResponse
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
                    Answers = question.QuestionType == QuestionType.Closed
                        ? question.Answers
                        : null,
                    CorrectAnswer = question.QuestionType == QuestionType.Closed
                        ? question.CorrectAnswer
                        : null
                }).ToList()
            }
        };
    }
}