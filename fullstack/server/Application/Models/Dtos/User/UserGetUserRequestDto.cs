using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.User;

public class UserGetUserRequestDto
{
    [Required] public string Email { get; set; } = null!;
}