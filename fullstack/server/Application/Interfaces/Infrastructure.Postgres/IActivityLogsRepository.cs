using Application.Models.Dtos.ActivityLog;
using Core.Domain.Entities;

namespace Application.Interfaces.Infrastructure.Postgres;

public interface IActivityLogsRepository
{
    Task CreateActivityLogs(ActivityLog activityLog);
    
    Task<List<ActivityLog>> GetActivityLogs();
    
    Task<List<ActivityLog>> GetLatestActivityLogs();
    
}