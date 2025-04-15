using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;
using Application.Interfaces.Infrastructure.Postgres;
using Application.Models;
using Application.Models.Dtos;
using Core.Domain.Entities;
using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Serializers;
using Microsoft.Extensions.Options;

namespace Application.Services;

public class SecurityService(IOptionsMonitor<AppOptions> optionsMonitor, IAuthDataRepository repository) : ISecurityService
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

    public AuthResponseDto Register(AuthRegisterRequestDto dto)
    {
        var user = repository.GetUserOrNull(dto.Email);
        if (user is not null) throw new ValidationException("User already exists");
        var salt = GenerateSalt();
        var hash = HashPassword(dto.Password + salt);
        var userRole = repository.GetRole("User");
        var insertedUser = repository.AddUser(new User
        {
            Id = Guid.NewGuid().ToString(),
            Email = dto.Email,
            Role = userRole,
            RoleId = userRole.Id,
            Salt = salt,
            HashedPassword = hash,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            
        });
        return new AuthResponseDto
        {
            Jwt = GenerateJwt(new JwtClaims
            {
                Id = insertedUser.Id,
                Role = insertedUser.Role.Name,
                Exp = DateTimeOffset.UtcNow.AddHours(1000).ToUnixTimeSeconds().ToString(),
                Email = insertedUser.Email
            })
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