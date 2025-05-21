using Application.Models.Dtos.ActivityLog;

namespace Application.Interfaces;

public interface IActivityLogService
{
    List<ActivityLogResponseDto> GetAllActivityLogs();
    
    List<ActivityLogResponseDto> GetLatestActivityLogs();
}