using Application.Models;
using Application.Models.Dtos;
using Application.Models.Dtos.Auth;
using Application.Models.Dtos.Auth.Invite;
using Application.Models.Dtos.Auth.Password;
using Application.Models.Dtos.User;

namespace Application.Interfaces;

public interface ISecurityService
{
    public string HashPassword(string password);
    public void VerifyPasswordOrThrow(string password, string hashedPassword);
    public string GenerateSalt();
    public string GenerateJwt(JwtClaims claims);
    public AuthResponseDto Login(AuthLoginRequestDto dto);
    public Task<UserResponseDto> Register(AuthRegisterRequestDto dto);
    public Task<AccountActivationResponseDto> AccountActivation(AccountActivationRequestDto dto);
    public Task<ResendInviteEmailResponseDto> ResendInviteEmail(ResendInviteEmailRequestDto dto);
    public Task<ForgotPasswordResponseDto> ForgotPassword(ForgotPasswordRequestDto dto);
    public Task<ResetPasswordResponseDto> ResetPassword(ResetPasswordRequestDto dto);
    public JwtClaims VerifyJwtOrThrow(string jwt);
}