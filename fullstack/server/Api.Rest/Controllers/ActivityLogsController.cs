using Application.Interfaces;
using Application.Models.Dtos.ActivityLog;
using Microsoft.AspNetCore.Mvc;

namespace Api.Rest.Controllers;

[ApiController]
public class ActivityLogsController(IActivityLogService activityLogService, ISecurityService securityService)
    : ControllerBase
{
    public const string ControllerRoute = "api/activity-logs/";

    public const string GetAllActivityLogsRoute = ControllerRoute + nameof(GetAllActivityLogs);

    public const string GetLatestActivityLogsRoute = ControllerRoute + nameof(GetLatestActivityLogs);

    [HttpGet]
    [Route(GetAllActivityLogsRoute)]
    public async Task<ActionResult<List<ActivityLogResponseDto>>> GetAllActivityLogs([FromHeader] string authorization)
    {
        var jwt = securityService.VerifyJwtOrThrow(authorization);
        if (jwt.Role != "Admin") return Unauthorized();
        return Ok(await activityLogService.GetAllActivityLogs());
    }

    [HttpGet]
    [Route(GetLatestActivityLogsRoute)]
    public async Task<ActionResult<List<ActivityLogResponseDto>>> GetLatestActivityLogs(
        [FromHeader] string authorization)
    {
        var jwt = securityService.VerifyJwtOrThrow(authorization);
        if (jwt.Role != "Admin") return Unauthorized();
        return Ok(await activityLogService.GetLatestActivityLogs());
    }
}