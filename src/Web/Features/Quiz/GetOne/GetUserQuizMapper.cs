using Core.DataAccess;
using Core.Entities;
using FastEndpoints;
using Web.Features.Quiz.GetAll;

namespace Web.Features.Quiz.GetOne;

public class GetUserQuizMapper : ResponseMapper<GetUserQuizResponse, Core.Entities.Quiz>
{
    public override GetUserQuizResponse FromEntity(Core.Entities.Quiz entity)
    {
        var filesService = Resolve<IFilesService>();
        return new GetUserQuizResponse
        {
            Quiz = new QuizDto 
            {
                Id = entity.Id,
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