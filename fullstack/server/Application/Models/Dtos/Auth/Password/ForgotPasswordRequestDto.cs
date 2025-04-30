using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Auth.Password;

public class ForgotPasswordRequestDto
{
    [Required] public string Email { get; set; } = null!;
}