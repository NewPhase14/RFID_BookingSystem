using Application.Interfaces;
using Application.Interfaces.Infrastructure.Postgres;
using Application.Models.Dtos.ActivityLog;

namespace Application.Services;

public class ActivityLogService(IActivityLogsRepository activityLogsRepository) : IActivityLogService
{
    public List<ActivityLogResponseDto> GetAllActivityLogs()
    {
        var activityLogs = activityLogsRepository.GetActivityLogs();

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

    public List<ActivityLogResponseDto> GetLatestActivityLogs()
    {
        var latestActivityLogs = activityLogsRepository.GetLatestActivityLogs();

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