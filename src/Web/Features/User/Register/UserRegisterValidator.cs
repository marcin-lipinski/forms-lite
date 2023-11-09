using FastEndpoints;
using FluentValidation;

namespace Web.Features.User.Register;

public class UserRegisterValidator : Validator<UserRegisterRequest>
{
    public UserRegisterValidator()
    {
        RuleFor(u => u.Username)
            .NotEmpty().WithMessage("Login cannot be empty")
            .NotNull().WithMessage("Login cannot be empty")
            .MinimumLength(6).WithMessage("Login is too short. Minimum size is 6.")
            .MaximumLength(25).WithMessage("Login is too long. Maximum size is 25");
        
        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("Password cannot be empty")
            .NotNull().WithMessage("Password cannot be empty")
            .MinimumLength(10).WithMessage("The password must be at least 10 characters long.")
            .Must(s => s.Any(ch => !char.IsLetterOrDigit(ch))).WithMessage("The password must contain at least 1 special character.")
            .Must(s => s.Any(char.IsUpper)).WithMessage("The password must contain at least 1 upper letter.")
            .Must(s => s.Count(char.IsNumber) > 3).WithMessage("The password must contain at least 4 numbers.");;
        
        RuleFor(u => u.PasswordRepeat)
            .NotEmpty().WithMessage("PasswordRepeat cannot be empty")
            .NotNull().WithMessage("PasswordRepeat cannot be empty")
            .Equal(u => u.Password).WithMessage("Passwords are not the same.");
    }
}