namespace Application.Models.Dtos.Auth.Invite;

public class AccountActivationRequestDto
{
    public string TokenId { get; set; } = null!;
    public string Password { get; set; } = null!;
}