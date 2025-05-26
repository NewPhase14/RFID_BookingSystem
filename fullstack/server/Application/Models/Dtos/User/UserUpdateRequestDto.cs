namespace Application.Models.Dtos.User;

public class UserUpdateRequestDto
{
    public string Id { get; set; } = null!;
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Rfid { get; set; }
}