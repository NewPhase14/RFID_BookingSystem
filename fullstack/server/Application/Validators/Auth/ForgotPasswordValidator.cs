using Application.Models.Dtos.Auth.Password;
using FluentValidation;

namespace Application.Validators.Auth;

public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordRequestDto>
{
    public ForgotPasswordValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}