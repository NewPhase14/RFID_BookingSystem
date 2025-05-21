using Application.Models.Dtos.ActivityLog;
using Core.Domain.Entities;

namespace Application.Interfaces.Infrastructure.Postgres;

public interface IActivityLogsRepository
{
    void CreateActivityLogs(ActivityLog activityLog);
    
    List<ActivityLog> GetActivityLogs();
    
    List<ActivityLog> GetLatestActivityLogs();
    
}