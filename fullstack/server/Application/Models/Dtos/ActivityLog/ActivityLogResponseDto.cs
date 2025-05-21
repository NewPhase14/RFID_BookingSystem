namespace Application.Models.Dtos.ActivityLog;

public class ActivityLogResponseDto
{
    public string Id { get; set; } = null!;
    public string ServiceName { get; set; } = null!;
    public string Date { get; set; } = null!;
    public string Time { get; set; } = null!;
    public string Fullname { get; set; } = null!;
    public string Status { get; set; } = null!;
}