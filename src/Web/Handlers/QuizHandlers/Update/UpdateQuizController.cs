using Core.Entities.Quiz;
using Core.Exceptions;
using Core.Exceptions.Quiz;
using Core.Exceptions.Security;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Services.Interfaces;

namespace Web.Handlers.QuizHandlers.Update;

public class UpdateQuizController : Controller
{
    private readonly IDbContext _dbContext;
    private readonly IFilesService _filesService;
    private readonly IUserAccessor _userAccessor;
    private readonly IValidator<UpdateQuizRequest> _validator;
    
    public UpdateQuizController(IDbContext dbContext, IFilesService filesService, IUserAccessor userAccessor, IValidator<UpdateQuizRequest> validator)
    {
        _dbContext = dbContext;
        _filesService = filesService;
        _userAccessor = userAccessor;
        _validator = validator;
    }

    [HttpPost("/api/quiz/update/{quizId}")]
    public async Task<IActionResult> Update([FromForm]UpdateQuizRequest request, CancellationToken ct, string quizId)
    {
        var validationResult = await _validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid) throw new VException(validationResult.ToDictionary());
        
        var userId = _userAccessor.GetUserId();
        if (string.IsNullOrEmpty(userId)) throw new UnauthorizedException();

        var originalQuiz = _dbContext.Collection<Quiz>().AsQueryable().SingleOrDefault(q => q.Id == quizId);
        if (originalQuiz is null) throw new NotFoundException("Quiz");
        
        var quiz = request.Map();
        quiz.Id = request.ReplacePreviousVersion ? originalQuiz.Id : ObjectId.GenerateNewId().ToString();
        quiz.AuthorId = originalQuiz.AuthorId;
        quiz.Title = _dbContext.Collection<Quiz>().AsQueryable().Any(q => q.Title == request.Quiz.Title)
            ? originalQuiz.Title + " copy"
            : request.Quiz.Title;
        
        foreach (var question in quiz.Questions)
        {
            var originalQuestion = originalQuiz.Questions.FirstOrDefault(q => q.Id == question.Id);
            var requestQuestion = request.Quiz.Questions.SingleOrDefault(q => q.Id == question.Id);

            if (requestQuestion is { Image: not null })
            {
                question.Image = await _filesService.SaveImage(quiz.Title, question.Id, requestQuestion.Image);
            }
            else if (requestQuestion != null && !string.IsNullOrEmpty(requestQuestion.ContentImageUrl))
            {
                if(originalQuestion is { Image: not null }) question.Image = originalQuestion.Image;
            }
        }

        if (request.ReplacePreviousVersion)
        {
            await _dbContext.Collection<Quiz>().ReplaceOneAsync(q => q.Id == quizId, quiz, cancellationToken: ct);
            return Ok(new UpdateQuizResponse{QuizId = quiz.Id});
        }

        await _dbContext.Collection<Quiz>().InsertOneAsync(quiz, cancellationToken: ct);
        return Ok(new UpdateQuizResponse{QuizId = quiz.Id});
    }
}