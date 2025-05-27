namespace Core.Domain.Entities;

public class ServiceAvailability
{
    public string Id { get; set; } = null!;

    public string ServiceId { get; set; } = null!;

    public int DayOfWeek { get; set; }

    public TimeOnly AvailableFrom { get; set; }

    public TimeOnly AvailableTo { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Service Service { get; set; } = null!;
}