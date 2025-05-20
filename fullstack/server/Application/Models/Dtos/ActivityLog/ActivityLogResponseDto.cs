namespace Application.Models.Dtos.ActivityLog;

public class ActivityLogsResponseDto
{
    public string eventType { get; set; } 
    public string requestId { get; set; }
    public List<ActivityLogDto> activityLogs { get; set; }
}

public class ActivityLogDto
{
    public string Id { get; set; }
    public string ServiceName { get; set; }
    public string Date { get; set; }
    public string Time { get; set; }
    public string Fullname { get; set; }
    public string Status { get; set; }
}