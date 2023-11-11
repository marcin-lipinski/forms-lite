using Core.DataAccess;
using Core.Entities;
using FastEndpoints;
using Web.Features.Quiz.GetAll;

namespace Web.Features.Session.Partake;

public class PartakeSessionMapper : Mapper<PartakeSessionRequest, PartakeSessionResponse, Core.Entities.Quiz>
{
    public override PartakeSessionResponse FromEntity(Core.Entities.Quiz entity)
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
                        ? ((QuestionOpen)question).Answers
                        : null,
                    CorrectAnswer = question.QuestionType == QuestionType.Open
                        ? ((QuestionOpen)question).CorrectAnswer
                        : null
                }).ToList()
            }
        };
    }
}