using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.TimeSlot;

public class TimeSlotCreateRequestDto
{
    [Required] public string ServiceId { get; set; } = null!;
    [Required] public string WeekdayId { get; set; }
    [Required] public TimeOnly StartTime { get; set; }
    [Required] public TimeOnly EndTime { get; set; }
}