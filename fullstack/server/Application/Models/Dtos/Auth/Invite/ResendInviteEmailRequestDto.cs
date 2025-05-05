using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Auth.Invite;

public class ResendInviteEmailRequestDto
{
    [Required] public string Email { get; set; } = null!;
}