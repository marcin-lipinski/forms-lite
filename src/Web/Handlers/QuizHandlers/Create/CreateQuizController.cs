using Core.Entities.Quiz;
using Core.Exceptions;
using Core.Exceptions.Quiz;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Services.Interfaces;

namespace Web.Handlers.QuizHandlers.Create;
public class CreateQuizController : Controller
{
    private readonly IDbContext _dbContext;
    private readonly IFilesService _filesService;
    private readonly IUserAccessor _userAccessor;
    private readonly IValidator<CreateQuizRequest> _validator;
    
    public CreateQuizController(IDbContext dbContext, IFilesService filesService, IUserAccessor userAccessor, IValidator<CreateQuizRequest> validator)
    {
        _dbContext = dbContext;
        _filesService = filesService;
        _userAccessor = userAccessor;
        _validator = validator;
    }

    [HttpPost("/api/quiz/create")]
    public async Task<IActionResult> Create([FromForm]CreateQuizRequest request, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid) throw new VException(validationResult.ToDictionary());

        var userId = _userAccessor.GetUserId();
        if (IsQuizTitleTaken(request.Quiz.Title, userId)) throw new QuizTitleTakenException();
        
        var quiz = request.Map();
        quiz.AuthorId = _userAccessor.GetUserId();
        foreach (var question in quiz.Questions)
        {
            var outImage = request.Quiz.Questions.SingleOrDefault(q => q.Id == question.Id)?.Image;
            if (outImage is not null)
            {
                question.Image = await _filesService.SaveImage(quiz.Title, question.Id, outImage);
            }
        }
        
        quiz.Id = ObjectId.GenerateNewId().ToString();
        await _dbContext.Collection<Quiz>().InsertOneAsync(quiz, cancellationToken: ct);
        return Ok(new CreateQuizResponse{QuizId = quiz.Id});
    }

    private bool IsQuizTitleTaken(string title, string userId) => _dbContext.Collection<Quiz>()
        .AsQueryable().FirstOrDefault(q => q.Title == title && q.AuthorId == userId) != null;
}