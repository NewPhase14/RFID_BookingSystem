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
    
    public EmailVerificationToken AddEmailVerificationToken(EmailVerificationToken token)
    {
        ctx.EmailVerificationTokens.Add(token);
        ctx.SaveChanges();
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
}