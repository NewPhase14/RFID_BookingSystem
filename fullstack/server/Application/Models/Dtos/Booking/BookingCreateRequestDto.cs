using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Booking;

public class BookingCreateRequestDto
{
    [Required] public string UserId { get; set; } = null!;
    [Required] public string ServiceId { get; set; } = null!; 
    [Required] public DateOnly Date { get; set; }
    [Required] public TimeOnly StartTime  { get; set; }
    [Required] public TimeOnly EndTime { get; set; }
}