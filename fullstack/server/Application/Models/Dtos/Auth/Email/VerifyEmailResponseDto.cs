using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Auth.Email;

public class VerifyEmailResponseDto
{ 
    public string Message { get; set; } = null!;
}