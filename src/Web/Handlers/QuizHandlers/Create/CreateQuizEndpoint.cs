

using System.Text.Json;
using Core.Entities.Question;
using Core.Entities.Quiz;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Services.Interfaces;

namespace Web.Handlers.QuizHandlers.Create;
public class CreateQuizController : Controller
{
    private readonly IDbContext DbContext;
    private readonly IFilesService FilesService;
    private readonly IUserAccessor UserAccessor;

    public CreateQuizController(IDbContext dbContext, IFilesService filesService, IUserAccessor userAccessor)
    {
        DbContext = dbContext;
        FilesService = filesService;
        UserAccessor = userAccessor;
    }

    [HttpPost("/api/quiz/create")]
    public async Task<IActionResult> CreateQuiz([FromForm] CreateQuizRequest formData)
    {
        var quiz = new Quiz
        {
            AuthorId = UserAccessor.GetUserId(),
            Title = formData.Quiz.Title,
            Version = 0,
            Questions = formData.Quiz.Questions.Select(question => question.QuestionType == QuestionType.Closed 
                ? new Question
                {
                    ContentText = question.ContentText,
                    QuestionNumber = question.QuestionNumber,
                    QuestionType = QuestionType.Closed
                }
                : new Question
                {
                    ContentText = question.ContentText,
                    QuestionNumber = question.QuestionNumber,
                    QuestionType = QuestionType.Open,
                    Answers = question.Answers != null ? question.Answers.ToList() : new System.Collections.Generic.List<string>(),
                    CorrectAnswer = question.CorrectAnswer
                }).ToList()
        };
        
        foreach (var question in quiz.Questions)
        {
            var outImage = formData.Quiz.Questions.SingleOrDefault(q => q.QuestionNumber == question.QuestionNumber)?.Image;
            if (outImage is not null)
            {
                question.Image = await FilesService.SaveImage(quiz.Title, question.QuestionNumber, outImage);
            }
        }

        quiz.Id = ObjectId.GenerateNewId().ToString();
        await DbContext.Collection<Quiz>().InsertOneAsync(quiz);
        return Ok(new CreateQuizResponse{QuizId = quiz.Id});
    }
}