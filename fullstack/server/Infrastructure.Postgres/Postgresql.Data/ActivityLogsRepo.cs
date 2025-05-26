using Application.Interfaces.Infrastructure.Postgres;
using Application.Models.Dtos.ActivityLog;
using Core.Domain.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Postgres.Postgresql.Data;

public class ActivityLogsRepo(MyDbContext ctx) : IActivityLogsRepository
{
    public async Task CreateActivityLogs(ActivityLog activityLog)
    {
        ctx.ActivityLogs.Add(activityLog);
    
        await ctx.SaveChangesAsync();
    }

    public async Task<List<ActivityLog>> GetActivityLogs()
    {
        var activityLogs = await ctx.ActivityLogs
            .Include(u => u.User)
            .Include(s => s.Service)
            .OrderByDescending(x => x.AttemptedAt).ToListAsync();
        return activityLogs;
    }
    
    public async Task<List<ActivityLog>> GetLatestActivityLogs()
    {
        var activityLogs = await ctx.ActivityLogs
            .Include(u => u.User)
            .Include(s => s.Service)
            .OrderByDescending(x => x.AttemptedAt)
            .Take(5)
            .ToListAsync();
        
        return activityLogs;
    }
    
}