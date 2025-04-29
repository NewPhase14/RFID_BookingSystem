using Application.Models.Dtos;
using Application.Models.Dtos.Auth;
using Core.Domain.Entities;

namespace Application.Interfaces.Infrastructure.Postgres;

public interface IAuthDataRepository
{
    User? GetUserOrNull(string email);
    User AddUser(User user);
    Role GetRole(string roleName);
    Task<EmailVerificationToken> AddEmailVerificationToken(EmailVerificationToken token); 
    Task<VerifyEmailResponseDto> VerifyEmail(VerifyEmailRequestDto dto);
    PasswordResetToken AddPasswordResetToken(PasswordResetToken token);
    Task<CheckEmailVerificationResponseDto> IsEmailVerified(CheckEmilVerificationRequestDto dto);
    Task RemoveExpiredEmailVerificationToken(string userId);
}