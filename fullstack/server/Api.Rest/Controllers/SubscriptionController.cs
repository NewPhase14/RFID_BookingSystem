using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Rest.Controllers;

[ApiController]
public class SubscriptionController(ISubscriptionService subscriptionService, ISecurityService securityService) : ControllerBase
{
    public const string SubscribeToDashboardRoute = nameof(SubscribeToDashboard);
    
    [HttpPost] 
    [Route(SubscribeToDashboardRoute)]
    public async Task<ActionResult> SubscribeToDashboard([FromHeader] string authorization, string clientId)
    {
        var jwt = securityService.VerifyJwtOrThrow(authorization);
        if (jwt.Role != "Admin")
        {
            return Unauthorized();
        }
        await subscriptionService.SubscribeToDashboard(clientId);
        return Ok();
    }
}