using Application.Interfaces;
using Application.Models.Dtos.ActivityLog;
using Microsoft.AspNetCore.Mvc;

namespace Api.Rest.Controllers;

[ApiController]
public class ActivityLogsController(IActivityLogService activityLogService) : ControllerBase
{
    public const string ControllerRoute = "api/activity-logs/";
    
    public const string GetActivityLogsRoute = ControllerRoute + nameof(GetActivityLogs);
    
    [HttpGet]
    [Route(GetActivityLogsRoute)]
    public ActionResult<List<ActivityLogDto>> GetActivityLogs()
    {
        return Ok(activityLogService.GetActivityLogs());
    }
    
}