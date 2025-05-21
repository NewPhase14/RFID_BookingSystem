using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Booking;

public class BookingsBroadcastDto
{
    public string eventType { get; set; }
    public string requestId { get; set; }
    public List<BookingResponseDto> bookings { get; set; } = null!;
}

public class BookingResponseDto
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string ServiceName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Date { get; set; } = null!;
    public string StartTime { get; set; } = null!;
    public string EndTime { get; set; } = null!;
    public string UpdatedAt { get; set; } = null!;
    public string CreatedAt { get; set; } = null!;
}