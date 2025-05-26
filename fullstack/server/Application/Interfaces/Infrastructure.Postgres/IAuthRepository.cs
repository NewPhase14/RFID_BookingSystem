using Application.Models.Dtos;
using Application.Models.Dtos.Auth;
using Application.Models.Dtos.Auth.Invite;
using Application.Models.Dtos.Auth.Password;
using Core.Domain.Entities;

namespace Application.Interfaces.Infrastructure.Postgres;

public interface IAuthRepository
{
    User? GetUserOrNull(string email);
    Task<User?> GetUserOrNullByPasswordResetTokenId(string tokenId);
    Task<User?> GetUserOrNullByInviteTokenId(string tokenId);
    User AddUser(User user);
    void UpdateUser(User user);
    Role GetRole(string roleName);
    Task<InviteToken> AddInviteToken(InviteToken token); 
    Task<VerifyInviteEmailResponseDto> VerifyInviteToken(VerifyInviteEmailRequestDto dto);
    Task<PasswordResetToken> AddPasswordResetToken(PasswordResetToken token);
    Task<VerifyPasswordTokenResponseDto> VerifyPasswordToken(VerifyPasswordTokenRequestDto dto);
    Task RemovePreviousInviteToken(string userId);
    Task RemovePreviousPasswordResetToken(string userId);
    Task RemovePasswordResetToken(string tokenId);
    Task RemoveInviteToken(string tokenId);
}