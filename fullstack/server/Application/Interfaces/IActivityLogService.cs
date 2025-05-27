using Application.Models.Dtos.ActivityLog;

namespace Application.Interfaces;

public interface IActivityLogService
{
    Task<List<ActivityLogResponseDto>> GetAllActivityLogs();

    Task<List<ActivityLogResponseDto>> GetLatestActivityLogs();
}