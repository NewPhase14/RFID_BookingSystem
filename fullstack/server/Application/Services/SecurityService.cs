using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;
using Application.Interfaces.Infrastructure.Postgres;
using Application.Interfaces.Infrastructure.Websocket;
using Application.Models;
using Application.Models.Dtos;
using Application.Models.Dtos.Auth;
using Application.Models.Dtos.Auth.Invite;
using Application.Models.Dtos.Auth.Password;
using Application.Models.Dtos.User;
using Core.Domain.Entities;
using FluentEmail.Core;
using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Serializers;
using Microsoft.Extensions.Options;

namespace Application.Services;

public class SecurityService(
    IOptionsMonitor<AppOptions> optionsMonitor,
    IAuthRepository repository,
    IFluentEmail fluentEmail,
    IConnectionManager connectionManager) : ISecurityService
{
    public async Task<AuthResponseDto> Login(AuthLoginRequestDto dto)
    {
        var user = await repository.GetUserOrNull(dto.Email) ?? throw new ValidationException("Username not found");

        if (user.ConfirmedEmail == false) throw new ValidationException("Account must be activated before logging in");

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

    public async Task<UserResponseDto> Register(AuthRegisterRequestDto dto)
    {
        var existingUser = await repository.GetUserOrNull(dto.Email);
        if (existingUser is not null) throw new ValidationException("User already exists");

        var randomPassword = Guid.NewGuid().ToString();

        var salt = GenerateSalt();
        var hash = HashPassword(randomPassword + salt);

        var userRole = await repository.GetRole(dto.Role);

        if (userRole is null) throw new ValidationException("User role not found");

        var normalizedFirstName = char.ToUpper(dto.FirstName[0]) + dto.FirstName[1..].ToLower();

        var normalizedLastName = char.ToUpper(dto.LastName[0]) + dto.LastName[1..].ToLower();
        
        var europeanTime = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
        var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, europeanTime);

        var newUser = new User
        {
            Id = Guid.NewGuid().ToString(),
            Rfid = dto.Rfid,
            Email = dto.Email.ToLower(),
            Role = userRole,
            RoleId = userRole.Id,
            Salt = salt,
            HashedPassword = hash,
            FirstName = normalizedFirstName,
            LastName = normalizedLastName,
            CreatedAt = now,
            UpdatedAt = now
        };

        try
        {
            newUser = await repository.AddUser(newUser);
        }
        catch (Exception e)
        {
            throw new ApplicationException("Failed to insert user into database", e);
        }


        var inviteToken = new InviteToken
        {
            Id = Guid.NewGuid().ToString(),
            UserId = newUser.Id,
            CreatedAt = now,
            ExpiresAt = now.AddDays(1)
        };


        var newToken = await repository.AddInviteToken(inviteToken);


        var verificationLink = $"https://bookit-rfid.web.app/activate?token={newToken.Id}";

        var email = fluentEmail
            .To(newUser.Email, $"{newUser.FirstName} {newUser.LastName}")
            .Subject("Account activation for bookit")
            .Body(
                $"Hello {newUser.FirstName}, thank you for registering at bookit. <br> To activate your account <a href='{verificationLink}'>click here</a>",
                true);

        var response = await email.SendAsync();

        if (!response.Successful) Console.WriteLine("Failed to send email.");

        return new UserResponseDto
        {
            Id = newUser.Id,
            Rfid = newUser.Rfid,
            Email = newUser.Email,
            FirstName = newUser.FirstName,
            LastName = newUser.LastName,
            RoleName = newUser.Role.Name,
            CreatedAt = newUser.CreatedAt.ToString("dd-MM-yyyy HH:mm"),
            UpdatedAt = newUser.UpdatedAt.ToString("dd-MM-yyyy HH:mm")
        };
    }

    public async Task<AccountActivationResponseDto> AccountActivation(AccountActivationRequestDto dto)
    {
        var europeanTime = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
        var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, europeanTime);
        
        var response = await repository.VerifyInviteToken(new VerifyInviteEmailRequestDto
        {
            TokenId = dto.TokenId
        });

        if (response.IsExpired) throw new ValidationException("Token is invalid or expired");

        Console.WriteLine("Token ID: " + dto.TokenId);

        var user = await repository.GetUserOrNullByInviteTokenId(dto.TokenId);

        if (user is null) throw new ValidationException("User not found");

        user.ConfirmedEmail = true;

        var salt = GenerateSalt();
        var hash = HashPassword(dto.Password + salt);

        user.HashedPassword = hash;
        user.Salt = salt;
        user.UpdatedAt = now;

        await repository.UpdateUser(user);

        await repository.RemoveInviteToken(dto.TokenId);

        return new AccountActivationResponseDto
        {
            Message = "Account was activated successfully."
        };
    }

    public async Task<ResendInviteEmailResponseDto> ResendInviteEmail(ResendInviteEmailRequestDto dto)
    {
        var europeanTime = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
        var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, europeanTime);
        
        var user = await repository.GetUserOrNull(dto.Email);
        if (user is null) throw new ValidationException("User not found");

        if (user.ConfirmedEmail == true) throw new ValidationException("Account already activated");

        //Delete previous tokens if they exist, no matter if they are expired or not
        await repository.RemovePreviousInviteToken(user.Id);

        var inviteToken = new InviteToken
        {
            Id = Guid.NewGuid().ToString(),
            UserId = user.Id,
            CreatedAt = now,
            ExpiresAt = now.AddDays(1)
        };

        var newToken = await repository.AddInviteToken(inviteToken);

        var verificationLink = $"https://bookit-rfid.web.app/activate?token={newToken.Id}";

        var email = fluentEmail
            .To(user.Email, $"{user.FirstName} {user.LastName}")
            .Subject("(Resend) Account activation for bookit")
            .Body(
                $"Hello {user.FirstName}, thank you for registering at bookit. <br> To activate your account <a href='{verificationLink}'>click here</a>",
                true);


        var response = await email.SendAsync();

        if (!response.Successful) throw new ApplicationException("Failed to send email.");

        return new ResendInviteEmailResponseDto
        {
            Message = "Activation email sent successfully."
        };
    }

    public async Task<ForgotPasswordResponseDto> ForgotPassword(ForgotPasswordRequestDto dto)
    {
        var europeanTime = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
        var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, europeanTime);
        
        var user = await repository.GetUserOrNull(dto.Email);
        if (user is null) throw new ValidationException("User not found");

        if (user.ConfirmedEmail == false)
            throw new ValidationException("Account must be activated before resetting password");

        //Delete previous tokens if they exist, no matter if they are expired or not
        await repository.RemovePreviousPasswordResetToken(user.Id);

        var token = new PasswordResetToken
        {
            Id = Guid.NewGuid().ToString(),
            UserId = user.Id,
            CreatedAt = now,
            ExpiresAt = now.AddDays(1)
        };

        var newToken = await repository.AddPasswordResetToken(token);

        var verificationLink = $"https://bookit-rfid.web.app/reset-password?token={newToken.Id}";

        var email = fluentEmail
            .To(dto.Email, $"{user.FirstName} {user.LastName}")
            .Subject("Reset password for bookit")
            .Body($"Hello {user.FirstName}, <br> To reset your password <a href='{verificationLink}'>click here</a>",
                true);

        var response = await email.SendAsync();

        if (!response.Successful) Console.WriteLine("Failed to send email.");

        return new ForgotPasswordResponseDto
        {
            Message = "Reset password email was sent successfully."
        };
    }

    public async Task<ResetPasswordResponseDto> ResetPassword(ResetPasswordRequestDto dto)
    {
        var europeanTime = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
        var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, europeanTime);
        
        var response = await repository.VerifyPasswordToken(new VerifyPasswordTokenRequestDto
        {
            TokenId = dto.TokenId
        });

        if (response.IsExpired) throw new ValidationException("Token is invalid or expired");

        var user = await repository.GetUserOrNullByPasswordResetTokenId(dto.TokenId);

        if (user is null) throw new ValidationException("User not found");

        var salt = GenerateSalt();
        var hash = HashPassword(dto.Password + salt);

        user.HashedPassword = hash;
        user.Salt = salt;
        user.UpdatedAt = now;

        await repository.UpdateUser(user);

        await repository.RemovePasswordResetToken(dto.TokenId);

        return new ResetPasswordResponseDto
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