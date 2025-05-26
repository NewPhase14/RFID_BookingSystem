using Application.Interfaces.Infrastructure.Postgres;
using Application.Models.Dtos;
using Application.Models.Dtos.Auth;
using Application.Models.Dtos.Auth.Invite;
using Application.Models.Dtos.Auth.Password;
using Core.Domain.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Postgres.Postgresql.Data;

public class AuthRepo(MyDbContext ctx) : IAuthRepository
{
    public async Task<User?> GetUserOrNull(string email)
    {
        return await ctx.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);
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

    public async Task<User> AddUser(User user)
    {
        ctx.Users.Add(user);
        await ctx.SaveChangesAsync();
        return user;
    }

    public async Task UpdateUser(User user)
    {
        ctx.Users.Update(user);
        await ctx.SaveChangesAsync();
    }

    public async Task<Role> GetRole(string roleName)
    {
        return await ctx.Roles.FirstOrDefaultAsync(r => r.Name == roleName) ?? throw new InvalidOperationException("Role not found");
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
                IsExpired = true
            };
        }

        return new VerifyInviteEmailResponseDto()
        {
            IsExpired = false
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
                IsExpired = true,
            };
        }

        return new VerifyPasswordTokenResponseDto()
        {
            IsExpired = false,
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