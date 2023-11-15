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
                    ContentText = question.ContentText,
                    ContentImageUrl = filesService.CreateImageUrl(question.Image.RelativePath),
                    QuestionNumber = question.QuestionNumber,
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