using Application.Interfaces.Infrastructure.Postgres;
using Application.Models.Dtos;
using Application.Models.Dtos.Auth;
using Application.Models.Dtos.Auth.Invite;
using Application.Models.Dtos.Auth.Password;
using Core.Domain.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Postgres.Postgresql.Data;

public class AuthRepo(MyDbContext ctx) : IAuthDataRepository
{
    public User? GetUserOrNull(string email)
    {
        return ctx.Users.Include(u => u.Role).FirstOrDefault(u => u.Email == email);
    }

    public async Task<User?> GetUserOrNullByPasswordResetTokenId(string tokenId)
    {
        var token = await ctx.PasswordResetTokens
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == tokenId);
        
        return token?.User;
    }

    public async Task<User?> GetUserOrNullByInviteTokenId(string tokenId)
    {
        var token = await ctx.InviteTokens
            .Include(i => i.User)
            .FirstOrDefaultAsync(i => i.Id == tokenId);
        
        return token?.User;
    }

    public User AddUser(User user)
    {
        ctx.Users.Add(user);
        ctx.SaveChanges();
        return user;
    }

    public void UpdateUser(User user)
    {
        ctx.Users.Update(user);
        ctx.SaveChanges();
    }

    public Role GetRole(string roleName)
    {
        return ctx.Roles.FirstOrDefault(r => r.Name == roleName) ?? throw new InvalidOperationException("Role not found");
    }
    
    public async Task<InviteToken> AddInviteToken(InviteToken token)
    {
        ctx.InviteTokens.Add(token);
        await ctx.SaveChangesAsync();
        return token;
    }

    public async Task<VerifyInviteEmailResponseDto> VerifyInviteToken(VerifyInviteEmailRequestDto dto)
    {
        var token = await ctx.InviteTokens
            .Include(e => e.User)
            .FirstOrDefaultAsync(e => e.Id == dto.TokenId);

        if (token is null || token.ExpiresAt < DateTime.Now)
        {
            return new VerifyInviteEmailResponseDto()
            {
                Message = "Token is invalid or expired."
            };
        }

        return new VerifyInviteEmailResponseDto()
        {
            Message = "invite token is valid.",
        };
    }

    public async Task<PasswordResetToken> AddPasswordResetToken(PasswordResetToken token)
    {
        ctx.PasswordResetTokens.Add(token);
        await ctx.SaveChangesAsync();
        return token;
    }

    public async Task<VerifyPasswordTokenResponseDto> VerifyPasswordToken(VerifyPasswordTokenRequestDto dto)
    {
        var token = await ctx.PasswordResetTokens
            .Include(u => u.User)
            .FirstOrDefaultAsync(e => e.Id == dto.TokenId);
        
        if (token is null || token.ExpiresAt < DateTime.Now)
        {
            return new VerifyPasswordTokenResponseDto()
            {
                IsValid = false,
                Message = "Token is invalid or expired."
            };
        }

        return new VerifyPasswordTokenResponseDto()
        {
            IsValid = true,
            Message = "Password has been reset successfully.",
        };
    }

    public async Task RemovePreviousInviteToken(string userId)
    {
        var existingToken = ctx.InviteTokens.FirstOrDefault(e => e.UserId == userId);
        if (existingToken != null) ctx.InviteTokens.Remove(existingToken);
        await ctx.SaveChangesAsync();
    }

    public async Task RemovePreviousPasswordResetToken(string userId)
    {
        var existingToken = ctx.PasswordResetTokens.FirstOrDefault(e => e.UserId == userId);
        if (existingToken != null) ctx.PasswordResetTokens.Remove(existingToken);
        await ctx.SaveChangesAsync();
    }

    public async Task RemovePasswordResetToken(string tokenId)
    {
        var token = await ctx.PasswordResetTokens.FirstOrDefaultAsync(e => e.Id == tokenId);
        if (token != null)
        {
            ctx.PasswordResetTokens.Remove(token);
            await ctx.SaveChangesAsync();
        }
    }

    public async Task RemoveInviteToken(string tokenId)
    {
        var token = await ctx.InviteTokens.FirstOrDefaultAsync(e => e.Id == tokenId);
        if (token != null)
        {
            ctx.InviteTokens.Remove(token);
            await ctx.SaveChangesAsync();
        }
    }
}