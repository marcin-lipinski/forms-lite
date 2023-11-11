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