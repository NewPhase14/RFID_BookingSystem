using Application.Models;
using Application.Models.Dtos;
using Application.Models.Dtos.Auth;
using Application.Models.Dtos.Auth.Invite;
using Application.Models.Dtos.Auth.Password;
using Application.Models.Dtos.User;

namespace Application.Interfaces;

public interface ISecurityService
{
    string HashPassword(string password);
    void VerifyPasswordOrThrow(string password, string hashedPassword);
    string GenerateSalt();
    string GenerateJwt(JwtClaims claims);
    Task<AuthResponseDto> Login(AuthLoginRequestDto dto);
    Task<UserResponseDto> Register(AuthRegisterRequestDto dto);
    Task<AccountActivationResponseDto> AccountActivation(AccountActivationRequestDto dto);
    Task<ResendInviteEmailResponseDto> ResendInviteEmail(ResendInviteEmailRequestDto dto);
    Task<ForgotPasswordResponseDto> ForgotPassword(ForgotPasswordRequestDto dto);
    Task<ResetPasswordResponseDto> ResetPassword(ResetPasswordRequestDto dto);
    JwtClaims VerifyJwtOrThrow(string jwt);
}