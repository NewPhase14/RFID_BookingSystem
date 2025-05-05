using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;
using Application.Interfaces.Infrastructure.Postgres;
using Application.Models;
using Application.Models.Dtos;
using Application.Models.Dtos.Auth;
using Application.Models.Dtos.Auth.Email;
using Application.Models.Dtos.Auth.Password;
using Core.Domain.Entities;
using FluentEmail.Core;
using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Serializers;
using Microsoft.Extensions.Options;

namespace Application.Services;

public class SecurityService(IOptionsMonitor<AppOptions> optionsMonitor, IAuthDataRepository repository, IFluentEmail fluentEmail) : ISecurityService
{
    public AuthResponseDto Login(AuthLoginRequestDto dto)
    {
        var user = repository.GetUserOrNull(dto.Email) ?? throw new ValidationException("Username not found");
        VerifyPasswordOrThrow(dto.Password + user.Salt, user.HashedPassword);
        
        var userRole = user.Role.Name;
        
        return new AuthResponseDto
        {
            Jwt = GenerateJwt(new JwtClaims
            {
                Id = user.Id,
                Role = userRole,
                Exp = DateTimeOffset.UtcNow.AddHours(1000)
                    .ToUnixTimeSeconds()
                    .ToString(),
                Email = dto.Email
            })
        };
    }

    public async Task<AuthResponseDto> Register(AuthRegisterRequestDto dto)
    {
        var existingUser = repository.GetUserOrNull(dto.Email);
        if (existingUser is not null) throw new ValidationException("User already exists");
        
        var randomPassword = Guid.NewGuid().ToString();
        
        var salt = GenerateSalt();
        var hash = HashPassword(randomPassword + salt);
        
        var userRole = repository.GetRole("User");
        if (userRole is null) throw new ValidationException("User role not found");
        
        var newUser = new User
        {
            Id = Guid.NewGuid().ToString(),
            Rfid = dto.Rfid,
            Email = dto.Email,
            Role = userRole,
            RoleId = userRole.Id,
            Salt = salt,
            HashedPassword = hash,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        try
        {
            newUser = repository.AddUser(newUser);
        }
        catch (Exception e)
        {
            throw new ApplicationException("Failed to insert user into database", e);
        }

        var verificationToken = repository.AddEmailVerificationToken(new EmailVerificationToken
        {
            Id = Guid.NewGuid().ToString(),
            UserId = newUser.Id,
            CreatedAt = DateTime.Now,
            ExpiresAt = DateTime.Now.AddDays(1)
        });

        //Insert real address when it's ready
        var verificationLink = $"localhost:5001/verify-email?token={verificationToken.Id}";
        
        var email = fluentEmail
            .To(newUser.Email, $"{newUser.FirstName} {newUser.LastName}")
            .Subject("Email verification for bookit")
            .Body($"Hello {newUser.FirstName}, thank you for registering at bookit. <br> To verify you email address <a href='{verificationLink}'>click here</a>", true);

        var response = await email.SendAsync();

        if (!response.Successful)
        {
            Console.WriteLine("Failed to send email.");
        }

        return new AuthResponseDto
        {
            Jwt = GenerateJwt(new JwtClaims
            {
                Id = newUser.Id,
                Role = newUser.Role.Name,
                Exp = DateTimeOffset.UtcNow.AddHours(1000).ToUnixTimeSeconds().ToString(),
                Email = newUser.Email
            })
        };
    }

    public async Task<VerifyEmailResponseDto> VerifyEmail(VerifyEmailRequestDto dto)
    {
        return await repository.VerifyEmail(dto);
    }

    public async Task<ResendVerificationEmailResponseDto> ResendVerificationEmail(ResendVerificationEmailRequestDto dto)
    {
        var user = repository.GetUserOrNull(dto.Email);
        if (user is null) throw new ValidationException("User not found");
        
        if (user.ConfirmedEmail == true) throw new ValidationException("User already verified");

        await repository.RemoveExpiredEmailVerificationToken(user.Id);

        var token = new EmailVerificationToken
        {
            Id = Guid.NewGuid().ToString(),
            UserId = user.Id,
            CreatedAt = DateTime.Now,
            ExpiresAt = DateTime.Now.AddDays(1)
        };

        await repository.AddEmailVerificationToken(token);
        
        var verificationLink = $"localhost:5001/verify-email?token={token.Id}";

        var email = fluentEmail
            .To(user.Email, $"{user.FirstName} {user.LastName}")
            .Subject("New email verification for bookit")
            .Body($"Hello {user.FirstName}, thank you for registering at bookit. <br> To verify you email address <a href='{verificationLink}'>click here</a>", true);
        
        var response = await email.SendAsync();
        
        if (!response.Successful) throw new ApplicationException("Failed to send email.");
        
        return new ResendVerificationEmailResponseDto()
        {
            Message = "Verification email sent successfully."
        };
    }

    public async Task<ForgotPasswordResponseDto> ForgotPassword(ForgotPasswordRequestDto dto)
    {
        var user = repository.GetUserOrNull(dto.Email);
        if (user is null) throw new ValidationException("User not found");
        
        if (user.ConfirmedEmail == false) throw new ValidationException("Email must be confirmed before resetting password");
        
        //Delete previous tokens if they exist, no matter if they are expired or not
        await repository.RemoveExpiredPasswordResetToken(user.Id);

        var token = new PasswordResetToken()
        {
            Id = Guid.NewGuid().ToString(),
            UserId = user.Id,
            CreatedAt = DateTime.Now,
            ExpiresAt = DateTime.Now.AddDays(1)
        };

        await repository.AddPasswordResetToken(token);
        
        var verificationLink = $"localhost:5001/password-reset?token={token.Id}";
        
        var email = fluentEmail
            .To(dto.Email, $"{user.FirstName} {user.LastName}")
            .Subject("Reset password for bookit")
            .Body($"Hello {user.FirstName}, <br> To reset your password <a href='{verificationLink}'>click here</a>", true);

        var response = await email.SendAsync();

        if (!response.Successful)
        {
            Console.WriteLine("Failed to send email.");
        }

        return new ForgotPasswordResponseDto()
        {
            Message = "Reset password email sent successfully."
        };
    }

    public async Task<ResetPasswordResponseDto> ResetPassword(ResetPasswordRequestDto dto)
    {

        await repository.VerifyPasswordToken(new VerifyPasswordTokenRequestDto()
        {
            TokenId = dto.TokenId
        });
        
        Console.WriteLine("Token ID: " + dto.TokenId);
        
        var user = await repository.GetUserOrNullByTokenId(dto.TokenId);
        
        if (user is null) throw new ValidationException("User not found");

        var salt = GenerateSalt();
        var hash = HashPassword(dto.Password + salt);
        
        user.HashedPassword = hash;
        user.Salt = salt;
        user.UpdatedAt = DateTime.Now;

        repository.UpdateUser(user);
        
        await repository.RemovePasswordResetToken(dto.TokenId);

        return new ResetPasswordResponseDto()
        {
            Message = "Password reset successfully."
        };
        
        
    }


    /// <summary>
    ///     Gives hex representation of SHA512 hash
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public string HashPassword(string password)
    {
        using var sha512 = SHA512.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha512.ComputeHash(bytes);
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }

    public void VerifyPasswordOrThrow(string password, string hashedPassword)
    {
        if (HashPassword(password) != hashedPassword)
            throw new AuthenticationException("Invalid login");
    }

    public string GenerateSalt()
    {
        return Guid.NewGuid().ToString();
    }

    public string GenerateJwt(JwtClaims claims)
    {
        var tokenBuilder = new JwtBuilder()
            .WithAlgorithm(new HMACSHA512Algorithm())
            .WithSecret(optionsMonitor.CurrentValue.JwtSecret)
            .WithUrlEncoder(new JwtBase64UrlEncoder())
            .WithJsonSerializer(new JsonNetSerializer());

        foreach (var claim in claims.GetType().GetProperties())
            tokenBuilder.AddClaim(claim.Name, claim.GetValue(claims)!.ToString());
        return tokenBuilder.Encode();
    }

    public JwtClaims VerifyJwtOrThrow(string jwt)
    {
        var token = new JwtBuilder()
            .WithAlgorithm(new HMACSHA512Algorithm())
            .WithSecret(optionsMonitor.CurrentValue.JwtSecret)
            .WithUrlEncoder(new JwtBase64UrlEncoder())
            .WithJsonSerializer(new JsonNetSerializer())
            .MustVerifySignature()
            .Decode<JwtClaims>(jwt);

        if (DateTimeOffset.FromUnixTimeSeconds(long.Parse(token.Exp)) < DateTimeOffset.UtcNow)
            throw new AuthenticationException("Token expired");
        return token;
    }
}