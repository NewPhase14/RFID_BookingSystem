using System;
using System.Collections.Generic;

namespace Core.Domain.Entities;

public partial class Booking
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string ServiceId { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<BookingLog> BookingLogs { get; set; } = new List<BookingLog>();

    public virtual Service Service { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
