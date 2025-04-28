using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Auth;

public class VerifyEmailRequestDto
{
    [Required] public string TokenId { get; set; } = null!;
}