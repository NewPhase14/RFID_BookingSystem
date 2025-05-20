using Application.Interfaces;
using Application.Interfaces.Infrastructure.Postgres;
using Application.Models.Dtos.ActivityLog;

namespace Application.Services;

public class ActivityLogService(IActivityLogsRepository activityLogsRepository) : IActivityLogService
{
    public List<ActivityLogDto> GetActivityLogs()
    {
        var activityLogs = activityLogsRepository.GetActivityLogs();
        var activityLogsDto = new List<ActivityLogDto>();

        foreach (var activityLog in activityLogs)
        {
            var activityLogDto = new ActivityLogDto
            {
                Id = activityLog.Id,
                Date = activityLog.AttemptedAt.ToString("dd-MM-yyyy"),
                Time = activityLog.AttemptedAt.ToString("HH:mm:ss"),
                ServiceName = activityLog.Service.Name,
                Fullname = activityLog.User.FirstName + " " + activityLog.User.LastName,
                Status = activityLog.Status
            };
            activityLogsDto.Add(activityLogDto);
        }

        return activityLogsDto ;
    }
    
}