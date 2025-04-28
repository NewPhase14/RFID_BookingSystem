using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;
using Application.Interfaces.Infrastructure.Postgres;
using Application.Models;
using Application.Models.Dtos;
using Application.Models.Dtos.Auth;
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
        
        var salt = GenerateSalt();
        var hash = HashPassword(dto.Password + salt);
        
        var userRole = repository.GetRole("User");
        if (userRole is null) throw new ValidationException("User role not found");
        
        var newUser = new User
        {
            Id = Guid.NewGuid().ToString(),
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