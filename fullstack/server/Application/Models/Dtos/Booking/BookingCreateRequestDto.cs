using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Booking;

public class BookingCreateRequestDto
{
    [Required] public string UserId { get; set; } = null!;
    [Required] public string ServiceId { get; set; } = null!;
    [Required] public string StatusId { get; set; } = null!;
    [Required] public string SlotId { get; set; } = null!;
    [Required] public DateOnly BookingDate { get; set; }
    
    
    
    
    
    
}