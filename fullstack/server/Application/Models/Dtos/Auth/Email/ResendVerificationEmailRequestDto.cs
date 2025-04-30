using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Auth.Email;

public class ResendVerificationEmailRequestDto
{
    [Required] public string Email { get; set; } = null!;
}