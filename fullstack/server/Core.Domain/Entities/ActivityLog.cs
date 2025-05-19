using System;
using System.Collections.Generic;

namespace Core.Domain.Entities;

public partial class ActivityLog
{
    public string Id { get; set; } = null!;

    public string BookingId { get; set; } = null!;

    public DateTime AttemptedAt { get; set; }

    public string UserId { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual Booking Booking { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
