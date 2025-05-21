using Application.Models.Dtos.ActivityLog;

namespace Application.Interfaces;

public interface IActivityLogService
{
    List<ActivityLogDto> GetActivityLogs();
    
    List<ActivityLogDto> GetLatestActivityLogs();
}