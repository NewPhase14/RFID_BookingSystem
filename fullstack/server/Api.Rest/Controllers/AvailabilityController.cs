using Application.Interfaces;
using Application.Models.Dtos;
using Application.Models.Dtos.Availability;
using Microsoft.AspNetCore.Mvc;

namespace Api.Rest.Controllers;

[ApiController]

public class AvailabilityController(IAvailabilityService availabilityService, ISecurityService securityService) : ControllerBase
{
    public const string ControllerRoute = "api/availability/";
    
    public const string CreateAvailabilityRoute = ControllerRoute + nameof(CreateAvailability);
    
    public const string CreateAllAvailabilitiesRoute = ControllerRoute + nameof(CreateAllAvailabilities);
    
    public const string DeleteAvailabilityRoute = ControllerRoute + nameof(DeleteAvailability);
    
    public const string UpdateAvailabilityRoute = ControllerRoute + nameof(UpdateAvailability);
    
    public const string GetAvailabilitySlotsRoute = ControllerRoute + nameof(GetAvailabilitySlots);



    [HttpPost]
    [Route(CreateAvailabilityRoute)]
    public async Task<ActionResult<AvailabilityResponseDto>> CreateAvailability([FromBody] AvailabilityCreateRequestDto dto, [FromHeader] string authorization)
    {
        var jwt = securityService.VerifyJwtOrThrow(authorization);
        if (jwt.Role != "Admin")
        {
            return Unauthorized();
        }
        return Ok(await availabilityService.CreateAvailability(dto));
    }
    
    [HttpPost]
    [Route(CreateAllAvailabilitiesRoute)]
    public async Task<ActionResult<List<AvailabilityResponseDto>>> CreateAllAvailabilities([FromBody] List<AvailabilityCreateRequestDto> dtos, [FromHeader] string authorization)
    {
        var jwt = securityService.VerifyJwtOrThrow(authorization);
        if (jwt.Role != "Admin")
        {
            return Unauthorized();
        }
        return Ok(await availabilityService.CreateAllAvailabilities(dtos));
    }
    
    
    [HttpDelete]
    [Route(DeleteAvailabilityRoute)]
    public async Task<ActionResult<AvailabilityResponseDto>> DeleteAvailability(string id, [FromHeader] string authorization)
    {
        var jwt = securityService.VerifyJwtOrThrow(authorization);
        if (jwt.Role != "Admin")
        {
            return Unauthorized();
        }
        return Ok(await availabilityService.DeleteAvailability(id));
    }
    
    [HttpPut]
    [Route(UpdateAvailabilityRoute)]
    public async Task<ActionResult<AvailabilityResponseDto>> UpdateAvailability([FromBody] AvailabilityUpdateRequestDto dto, [FromHeader] string authorization)
    {
        var jwt = securityService.VerifyJwtOrThrow(authorization);
        if (jwt.Role != "Admin")
        {
            return Unauthorized();
        }
        return Ok(await availabilityService.UpdateAvailability(dto));
    }
    
    [HttpGet]
    [Route(GetAvailabilitySlotsRoute)]
    public async Task<ActionResult<List<AvailabiltySlotsDto>>> GetAvailabilitySlots(string serviceId, [FromHeader] string authorization)
    {
        var jwt = securityService.VerifyJwtOrThrow(authorization);
        if (jwt.Role != "User")
        {
            return Unauthorized();
        }
        return Ok(await availabilityService.GetAvailabilitySlots(serviceId));
    }
}