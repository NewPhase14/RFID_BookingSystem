using Application.Models.Dtos;
using Application.Models.Dtos.Auth;
using Application.Models.Dtos.Auth.Email;
using Application.Models.Dtos.Auth.Password;
using Core.Domain.Entities;

namespace Application.Interfaces.Infrastructure.Postgres;

public interface IAuthDataRepository
{
    User? GetUserOrNull(string email);
    Task<User?> GetUserOrNullByTokenId(string tokenId);
    User AddUser(User user);
    void UpdateUser(User user);
    Role GetRole(string roleName);
    Task<EmailVerificationToken> AddEmailVerificationToken(EmailVerificationToken token); 
    Task<VerifyEmailResponseDto> VerifyEmail(VerifyEmailRequestDto dto);
    Task<PasswordResetToken> AddPasswordResetToken(PasswordResetToken token);
    Task<VerifyPasswordTokenResponseDto> VerifyPasswordToken(VerifyPasswordTokenRequestDto dto);
    Task<CheckEmailVerificationResponseDto> IsEmailVerified(CheckEmailVerificationRequestDto dto);
    Task RemoveExpiredEmailVerificationToken(string userId);
    Task RemoveExpiredPasswordResetToken(string userId);
    Task RemovePasswordResetToken(string tokenId);
}