using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Auth;

public class CheckEmilVerificationRequestDto
{
    [Required] public string Email { get; set; } = null!;
}