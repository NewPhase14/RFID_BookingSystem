using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Auth.Password;

public class VerifyPasswordTokenRequestDto
{
    [Required] public string TokenId { get; set; } = null!;
}