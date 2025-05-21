namespace Application.Models.Dtos.Availability;

public class AvailabiltySlotsDto
{
    public string Date { get; set; } = null!;
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}