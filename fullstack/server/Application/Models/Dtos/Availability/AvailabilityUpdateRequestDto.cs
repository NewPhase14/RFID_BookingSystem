namespace Application.Models.Dtos.Availability;

public class AvailabilityUpdateRequestDto
{
    public string Id { get; set; } = null!;
    public string ServiceId { get; set; } = null!;
    public TimeOnly AvailableFrom { get; set; }
    public TimeOnly AvailableTo { get; set; }
    public int DayOfWeek { get; set; }
}