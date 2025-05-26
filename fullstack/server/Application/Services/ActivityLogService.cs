using Application.Interfaces;
using Application.Interfaces.Infrastructure.Postgres;
using Application.Models.Dtos.ActivityLog;

namespace Application.Services;

public class ActivityLogService(IActivityLogsRepository activityLogsRepository) : IActivityLogService
{
    public async Task<List<ActivityLogResponseDto>> GetAllActivityLogs()
    {
        var activityLogs = await activityLogsRepository.GetActivityLogs();

        return activityLogs.Select(activityLog => new ActivityLogResponseDto
        {
            Id = activityLog.Id,
            Date = activityLog.AttemptedAt.ToString("dd-MM-yyyy"),
            Time = activityLog.AttemptedAt.ToString("HH:mm:ss"),
            ServiceName = activityLog.Service.Name,
            Fullname = activityLog.User.FirstName + " " + activityLog.User.LastName,
            Status = activityLog.Status
        }).ToList();
    }

    public async Task<List<ActivityLogResponseDto>> GetLatestActivityLogs()
    {
        var latestActivityLogs = await activityLogsRepository.GetLatestActivityLogs();

        return latestActivityLogs.Select(activityLog => new ActivityLogResponseDto
        {
            Id = activityLog.Id,
            Date = activityLog.AttemptedAt.ToString("dd-MM-yyyy"),
            Time = activityLog.AttemptedAt.ToString("HH:mm:ss"),
            ServiceName = activityLog.Service.Name,
            Fullname = activityLog.User.FirstName + " " + activityLog.User.LastName,
            Status = activityLog.Status
        }).ToList();
    }
}