using System;
using System.Collections.Generic;

namespace Core.Domain.Entities;

public partial class PasswordResetToken
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public virtual User User { get; set; } = null!;
}
