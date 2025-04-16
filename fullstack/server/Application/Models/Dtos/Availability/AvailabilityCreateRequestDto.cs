using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Availability;

public class AvailabilityCreateRequestDto
{
    [Required] public string ServiceId { get; set; } = null!;
    [Required] public int DayOfWeek { get; set; } 
    [Required] public TimeOnly AvailableFrom  { get; set; }
    [Required] public TimeOnly AvailableTo { get; set; }
    
}