using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Auth.Password;

public class ResetPasswordRequestDto
{
    [Required] public string TokenId { get; set; } = null!;
    [Required] public string Password { get; set; } = null!;
}