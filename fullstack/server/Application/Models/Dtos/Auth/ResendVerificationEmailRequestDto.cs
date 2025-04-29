using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Auth;

public class ResendVerificationEmailRequestDto
{
    [Required] public string Email { get; set; } = null!;
}