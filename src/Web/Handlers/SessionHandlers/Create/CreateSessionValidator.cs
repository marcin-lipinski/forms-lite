using FastEndpoints;
using FluentValidation;

namespace Web.Handlers.SessionHandlers.Create;

public class CreateSessionValidator : Validator<CreateSessionRequest>
{
    public CreateSessionValidator()
    {
        RuleFor(request => request.StartTime)
            .NotNull().WithMessage("Finish time not provided")
            .NotEmpty().WithMessage("Finish time not provided")
            .Length(16).WithMessage("Finish time should be in DD:MM:YYYY HH:MM format.")
            .Matches("\\d\\d-\\d\\d-\\d\\d\\d\\d \\d\\d:\\d\\d")
            .WithMessage("Finish time should be in DD:MM:YYYY HH:MM format.");
        
        RuleFor(request => request.FinishTime)
            .NotNull().WithMessage("Finish time not provided")
            .NotEmpty().WithMessage("Finish time not provided")
            .Length(16).WithMessage("Finish time should be in DD:MM:YYYY HH:MM format.")
            .Matches("\\d\\d-\\d\\d-\\d\\d\\d\\d \\d\\d:\\d\\d")
            .WithMessage("Finish time should be in DD:MM:YYYY HH:MM format.");
    }
}