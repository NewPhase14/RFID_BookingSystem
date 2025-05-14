namespace Application.Models.Dtos.User;

public class UserResponseDto
{
    public string Id { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Rfid { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string RoleName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}