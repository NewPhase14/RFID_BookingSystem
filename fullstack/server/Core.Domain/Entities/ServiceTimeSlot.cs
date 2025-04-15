using System;
using System.Collections.Generic;

namespace Core.Domain.Entities;

public partial class ServiceTimeSlot
{
    public string Id { get; set; } = null!;

    public string ServiceId { get; set; } = null!;

    public int DayOfWeek { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Weekday DayOfWeekNavigation { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
