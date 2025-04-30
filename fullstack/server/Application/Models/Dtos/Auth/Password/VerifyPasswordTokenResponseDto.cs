namespace Application.Models.Dtos.Auth.Password;

public class VerifyPasswordTokenResponseDto
{
    public bool IsValid { get; set; }
    public string Message { get; set; } = null!;
}