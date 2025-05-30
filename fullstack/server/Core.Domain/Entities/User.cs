﻿namespace Core.Domain.Entities;

public class User
{
    public string Id { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Rfid { get; set; } = null!;

    public string HashedPassword { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string RoleId { get; set; } = null!;

    public bool? ConfirmedEmail { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<InviteToken> InviteTokens { get; set; } = new List<InviteToken>();

    public virtual ICollection<PasswordResetToken> PasswordResetTokens { get; set; } = new List<PasswordResetToken>();

    public virtual Role Role { get; set; } = null!;
}