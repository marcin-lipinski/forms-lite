using Core.DataAccess;
using Core.Entities;
using FastEndpoints;

namespace Web.Features.Quiz.GetAll;

public class GetUserQuizzesMapper : ResponseMapper<GetUserQuizzesResponse, List<Core.Entities.Quiz>>
{
    public override GetUserQuizzesResponse FromEntity(List<Core.Entities.Quiz> entities)
    {
        var filesService = Resolve<IFilesService>();
        return new GetUserQuizzesResponse
        {
            Quizzes = entities.Select(entity => new QuizDto
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
                }).ToList()
        };
    }
}