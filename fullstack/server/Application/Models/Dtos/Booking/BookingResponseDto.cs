using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Booking;

public class BookingResponseDto
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string ServiceId { get; set; } = null!;
    public string Date { get; set; } = null!;
    public string StartTime { get; set; } = null!;
    public string EndTime { get; set; } = null!;
    public string UpdatedAt { get; set; } = null!;
    public string CreatedAt { get; set; } = null!;
}