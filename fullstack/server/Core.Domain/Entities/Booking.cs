namespace Core.Domain.Entities;

public class Booking
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string ServiceId { get; set; } = null!;

    public DateOnly Date { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Service Service { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}