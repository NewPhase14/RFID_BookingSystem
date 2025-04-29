using Application.Interfaces.Infrastructure.Postgres;
using Application.Models.Dtos;
using Application.Models.Dtos.Auth;
using Core.Domain.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Postgres.Postgresql.Data;

public class AuthRepo(MyDbContext ctx) : IAuthDataRepository
{
    public User? GetUserOrNull(string email)
    {
        return ctx.Users.FirstOrDefault(u => u.Email == email);
    }

    public User AddUser(User user)
    {
        ctx.Users.Add(user);
        ctx.SaveChanges();
        return user;
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

    public PasswordResetToken AddPasswordResetToken(PasswordResetToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<CheckEmailVerificationResponseDto> IsEmailVerified(CheckEmilVerificationRequestDto dto)
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
}