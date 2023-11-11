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