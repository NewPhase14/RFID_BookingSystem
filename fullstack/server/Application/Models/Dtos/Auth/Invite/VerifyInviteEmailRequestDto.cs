using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Auth.Invite;

public class VerifyInviteEmailRequestDto
{
    [Required] public string TokenId { get; set; } = null!;
}