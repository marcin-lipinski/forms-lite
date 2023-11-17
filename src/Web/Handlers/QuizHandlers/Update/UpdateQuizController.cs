

using Core.Entities.Quiz;
using Core.Exceptions;
using Core.Exceptions.Quiz;
using Core.Exceptions.Security;
using FluentValidation;
using Infrastructure.Persistence.Files;
using Infrastructure.Security;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Services.Interfaces;
using Web.Handlers.QuizHandlers.Create;

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

    [HttpPost("/api/quiz/update")]
    public async Task<IActionResult> Update([FromForm]UpdateQuizRequest request, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid) throw new VException(validationResult.ToDictionary());
        
        var userId = _userAccessor.GetUserId();
        if (string.IsNullOrEmpty(userId)) throw new UnauthorizedException();

        var originalQuiz = _dbContext.Collection<Quiz>().AsQueryable()
            .SingleOrDefault(q => q.Id == request.QuizId);
        if (originalQuiz is null) throw new NotFoundException("Quiz");
        
        var quiz = request.Map();
        quiz.Id = originalQuiz.Id;
        quiz.Title = originalQuiz.Title.Equals(request.Quiz.Title)
            ? originalQuiz.Title + " copy"
            : request.Quiz.Title;
        quiz.Version = request.ReplacePreviousVersion ? originalQuiz.Version + 1 : 0;
        foreach (var question in quiz.Questions)
        {
            var outImage = request.Quiz.Questions.SingleOrDefault(q => q.QuestionNumber == question.QuestionNumber)?.Image;
            if (outImage is not null)
            {
                question.Image = await _filesService.SaveImage(quiz.Title, question.QuestionNumber, outImage);
            }
        }

        if (request.ReplacePreviousVersion)
        {
            await _dbContext.Collection<Quiz>().ReplaceOneAsync(q => q.Id == quiz.Id, quiz, cancellationToken: ct);
            return Ok(new UpdateQuizResponse{QuizId = request.QuizId});
        }

        quiz.Id = ObjectId.GenerateNewId().ToString();
        await _dbContext.Collection<Quiz>().InsertOneAsync(quiz, cancellationToken: ct);
        return Ok(new UpdateQuizResponse{QuizId = quiz.Id});
    }
}