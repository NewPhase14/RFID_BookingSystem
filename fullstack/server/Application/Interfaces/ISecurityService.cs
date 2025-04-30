using Application.Models;
using Application.Models.Dtos;
using Application.Models.Dtos.Auth;
using Application.Models.Dtos.Auth.Email;
using Application.Models.Dtos.Auth.Password;

namespace Application.Interfaces;

public interface ISecurityService
{
    public string HashPassword(string password);
    public void VerifyPasswordOrThrow(string password, string hashedPassword);
    public string GenerateSalt();
    public string GenerateJwt(JwtClaims claims);
    public AuthResponseDto Login(AuthLoginRequestDto dto);
    public Task<AuthResponseDto> Register(AuthRegisterRequestDto dto);
    public Task<VerifyEmailResponseDto> VerifyEmail(VerifyEmailRequestDto dto);
    public Task<ResendVerificationEmailResponseDto> ResendVerificationEmail(ResendVerificationEmailRequestDto dto);
    public Task<ForgotPasswordResponseDto> ForgotPassword(ForgotPasswordRequestDto dto);
    public Task<ResetPasswordResponseDto> ResetPassword(ResetPasswordRequestDto dto);
    public JwtClaims VerifyJwtOrThrow(string jwt);
}