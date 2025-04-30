using Application.Interfaces.Infrastructure.Postgres;
using Application.Models.Dtos;
using Application.Models.Dtos.Auth;
using Application.Models.Dtos.Auth.Email;
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

    public async Task<User?> GetUserOrNullByTokenId(string tokenId)
    {
        var token = await ctx.PasswordResetTokens
            .Include(e => e.User)
            .FirstOrDefaultAsync(e => e.Id == tokenId);
        
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
        return ctx.Roles.FirstOrDefault(r => r.Name == roleName) ?? throw new InvalidOperationException();
    }
    
    public async Task<EmailVerificationToken> AddEmailVerificationToken(EmailVerificationToken token)
    {
        ctx.EmailVerificationTokens.Add(token);
        await ctx.SaveChangesAsync();
        return token;
    }

    public async Task<VerifyEmailResponseDto> VerifyEmail(VerifyEmailRequestDto dto)
    {
        var token = await ctx.EmailVerificationTokens
            .Include(e => e.User)
            .FirstOrDefaultAsync(e => e.Id == dto.TokenId);

        if (token is null || token.ExpiresAt < DateTime.Now)
        {
            return new VerifyEmailResponseDto()
            {
                Message = "Token is invalid or expired."
            };
        }

        token.User.ConfirmedEmail = true;
        ctx.EmailVerificationTokens.Remove(token);
        await ctx.SaveChangesAsync();

        return new VerifyEmailResponseDto()
        {
            Message = "Email verified successfully."
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
        
        await ctx.SaveChangesAsync();

        return new VerifyPasswordTokenResponseDto()
        {
            IsValid = true,
            Message = "Password has been reset successfully.",
        };
    }

    public async Task<CheckEmailVerificationResponseDto> IsEmailVerified(CheckEmailVerificationRequestDto dto)
    {
        var user = await ctx.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user is null) 
            throw new InvalidOperationException("User not found.");

        return new CheckEmailVerificationResponseDto
        {
            IsConfirmed = user.ConfirmedEmail
        };
    }

    public async Task RemoveExpiredEmailVerificationToken(string userId)
    {
        var existingToken = ctx.EmailVerificationTokens.FirstOrDefault(e => e.UserId == userId);
        if (existingToken != null) ctx.EmailVerificationTokens.Remove(existingToken);
        await ctx.SaveChangesAsync();
    }

    public async Task RemoveExpiredPasswordResetToken(string userId)
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
}