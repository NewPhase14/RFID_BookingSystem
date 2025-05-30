﻿namespace Core.Domain.Entities;

public class ActivityLog
{
    public string Id { get; set; } = null!;

    public string ServiceId { get; set; } = null!;

    public DateTime AttemptedAt { get; set; }

    public string UserId { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}