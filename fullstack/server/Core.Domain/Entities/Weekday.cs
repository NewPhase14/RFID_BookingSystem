using System;
using System.Collections.Generic;

namespace Core.Domain.Entities;

public partial class Weekday
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ServiceTimeSlot> ServiceTimeSlots { get; set; } = new List<ServiceTimeSlot>();
}
