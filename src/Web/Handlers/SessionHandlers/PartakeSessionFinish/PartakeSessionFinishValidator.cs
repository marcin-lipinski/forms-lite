using FastEndpoints;
using FluentValidation;

namespace Web.Handlers.SessionHandlers.PartakeSessionFinish;

public class PartakeSessionFinishValidator : Validator<PartakeSessionFinishRequest>
{
    public PartakeSessionFinishValidator()
    {
        RuleFor(request => request.Participant)
            .NotNull().WithMessage("Participant name not provided")
            .NotEmpty().WithMessage("Participant name not provided")
            .MinimumLength(6).WithMessage("Participant name is too short. Minimum size is 6.")
            .MaximumLength(40).WithMessage("Participant name is too long. Maximum size is 40");
    }
}