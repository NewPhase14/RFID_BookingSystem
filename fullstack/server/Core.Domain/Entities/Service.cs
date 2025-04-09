using System;
using System.Collections.Generic;

namespace Core.Domain.Entities;

public partial class Service
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
