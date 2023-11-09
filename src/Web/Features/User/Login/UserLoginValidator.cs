using FastEndpoints;
using FluentValidation;

namespace Web.Features.User.Login;

public class UserLoginValidator : Validator<UserLoginRequest>
{
    public UserLoginValidator()
    {
        RuleFor(request => request.Login)
            .NotEmpty().WithMessage("Login cannot be empty")
            .NotNull().WithMessage("Login cannot be empty");
        
        RuleFor(request => request.Password)
            .NotEmpty().WithMessage("Password cannot be empty")
            .NotNull().WithMessage("Password cannot be empty");
    }
}