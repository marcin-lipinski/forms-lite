using Core.Entities.Question;
using FastEndpoints;
using FluentValidation;

namespace Web.Handlers.QuizHandlers.Create;

public class CreateQuizValidator : AbstractValidator<CreateQuizRequest>
{
    public CreateQuizValidator()
    {
        RuleFor(request => request.Quiz)
            .NotEmpty().WithMessage("Quiz cannot be empty")
            .NotNull().WithMessage("Quiz cannot be empty");
        
        RuleFor(request => request.Quiz.Title)
            .NotEmpty().WithMessage("Quiz title cannot be empty")
            .NotNull().WithMessage("Quiz title cannot be empty")
            .MinimumLength(10).WithMessage("Quiz title is too short. Minimum size is 10.")
            .MaximumLength(40).WithMessage("Quiz title is too long. Maximum size is 40");

        RuleFor(request => request.Quiz.Questions)
            .NotNull().WithMessage("The list of questions can not be empty")
            .NotEmpty().WithMessage("The list of questions can not be empty")
            .Must(questions => questions.Length <= 20)
            .WithMessage("The list of questions cannot contain more than 20 questions.");

        RuleForEach(request => request.Quiz.Questions).SetValidator(new CreateQuizQuestionValidator());
    }
}

public class CreateQuizQuestionValidator : AbstractValidator<QuestionDto>
{
    public CreateQuizQuestionValidator()
    {
        RuleFor(question => question.QuestionNumber)
            .Must(number => number >= 1).WithMessage("Error while quiz creating occured. Try again");
        
        RuleFor(question => question.ContentText)
            .NotEmpty().WithMessage("Quiz content text cannot be empty")
            .NotNull().WithMessage("Quiz content text cannot be empty")
            .MinimumLength(10).WithMessage("Quiz content text is too short. Minimum size is 10.")
            .MaximumLength(50).WithMessage("Quiz content text is too long. Maximum size is 40");

        RuleFor(question => question)
            .NotEmpty().WithMessage("Answers cannot be empty")
            .NotNull().WithMessage("Answers cannot be empty")
            .Custom((questionDto, context) =>
            {
                if (questionDto.QuestionType != QuestionType.Closed) return;
                if (questionDto.Answers is not { Length: 4 })
                {
                    context.AddFailure("Answers", "For closed question type, answers should contain 4 elements.");
                }
                else if (questionDto.Answers.Distinct().Count() != questionDto.Answers.Length)
                {
                    context.AddFailure("Answers", "Answers should contain unique elements.");
                }
                else if (!questionDto.Answers.Contains(questionDto.CorrectAnswer))
                {
                    context.AddFailure("CorrectAnswer", "Correct answer should be present in answers.");
                }
            });
    }
}