using Application.Interfaces;
using Application.Models.Dtos;
using Application.Models.Dtos.Availability;
using Microsoft.AspNetCore.Mvc;

namespace Api.Rest.Controllers;

[ApiController]

public class AvailabilityController(IAvailabilityService availabilityService) : ControllerBase
{
    public const string ControllerRoute = "api/availability/";
    
    public const string CreateTimeSlotRoute = ControllerRoute + nameof(CreateAvailability);

    [HttpPost]
    [Route(CreateTimeSlotRoute)]
    public ActionResult<AuthResponseDto> CreateAvailability([FromBody] AvailabilityCreateRequestDto dto)
    {
        return Ok(availabilityService.CreateAvailability(dto));
    }
    
}