using Application.Interfaces.Infrastructure.Postgres;
using Application.Models.Dtos.ActivityLog;
using Core.Domain.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Postgres.Postgresql.Data;

public class ActivityLogsRepo(MyDbContext ctx) : IActivityLogsRepository
{
    public void CreateActivityLogs(ActivityLog activityLog)
    {
        ctx.ActivityLogs.Add(activityLog);
    
        ctx.SaveChanges();
    }

    public List<ActivityLog> GetActivityLogs()
    {
        var activityLogs = ctx.ActivityLogs
            .Include(u => u.User)
            .Include(s => s.Service)
            .OrderByDescending(x => x.AttemptedAt).ToList();
        return activityLogs;
    }
    
    public List<ActivityLog> GetLatestActivityLogs()
    {
        var activityLogs = ctx.ActivityLogs
            .Include(u => u.User)
            .Include(s => s.Service)
            .OrderByDescending(x => x.AttemptedAt)
            .Take(5)
            .ToList();
        
        return activityLogs;
    }
    
}