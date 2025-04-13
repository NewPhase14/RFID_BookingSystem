using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Booking;

public class BookingCreateRequestDto
{
    [Required] public string UserId { get; set; } = null!;
    [Required] public string ServiceId { get; set; } = null!;
    [Required] public string StatusId { get; set; } = null!;
    [Required] public DateTime StartTime { get; set; }
    [Required] public DateTime EndTime { get; set; }
}