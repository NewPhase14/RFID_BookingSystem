using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Auth;

public class VerifyEmailResponseDto
{
    [Required] public string Message { get; set; } = null!;
}