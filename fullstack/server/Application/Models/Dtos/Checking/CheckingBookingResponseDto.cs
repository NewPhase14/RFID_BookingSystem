namespace Application.Models.Dtos.Checking;

public class CheckingBookingResponseDto
{
    public bool IsValid { get; set; }
    public string ServiceId { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string Status { get; set; } = null!;

}