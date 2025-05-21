using Application.Interfaces;
using Application.Models.Dtos.ActivityLog;
using Microsoft.AspNetCore.Mvc;

namespace Api.Rest.Controllers;

[ApiController]
public class ActivityLogsController(IActivityLogService activityLogService) : ControllerBase
{
    public const string ControllerRoute = "api/activity-logs/";
    
    public const string GetAllActivityLogsRoute = ControllerRoute + nameof(GetAllActivityLogs);
    
    public const string GetLatestActivityLogsRoute = ControllerRoute + nameof(GetLatestActivityLogs);
    
    [HttpGet]
    [Route(GetAllActivityLogsRoute)]
    public ActionResult<List<ActivityLogResponseDto>> GetAllActivityLogs()
    {
        return Ok(activityLogService.GetAllActivityLogs());
    }
    
    [HttpGet]
    [Route(GetLatestActivityLogsRoute)]
    public ActionResult<List<ActivityLogResponseDto>> GetLatestActivityLogs()
    {
        return Ok(activityLogService.GetLatestActivityLogs());
    }
    
    
}