using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Booking;

public class BookingResponseDto
{
    [Required] public string Message { get; set; } = null!;
}