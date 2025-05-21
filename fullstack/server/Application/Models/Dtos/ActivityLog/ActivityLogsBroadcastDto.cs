namespace Application.Models.Dtos.ActivityLog;

public class ActivityLogsBroadcastDto
{
    public string eventType { get; set; } 
    public string requestId { get; set; }
    public List<ActivityLogResponseDto> activityLogs { get; set; }
}