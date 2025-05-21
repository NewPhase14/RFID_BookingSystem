using Application.Models.Dtos.Auth.Invite;
using FluentValidation;

namespace Application.Validators.Auth;

public class AccountActivationValidator : AbstractValidator<AccountActivationRequestDto>
{
    public AccountActivationValidator()
    {
        RuleFor(x => x.TokenId).NotEmpty().WithMessage("Token ID is required.");
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one number.")
            .Matches("[!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~]").WithMessage(
                "Password must contain at least one special character.");
    }
    
}