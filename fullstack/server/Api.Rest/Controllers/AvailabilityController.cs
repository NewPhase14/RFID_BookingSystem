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
    
    public const string DeleteTimeSlotRoute = ControllerRoute + nameof(DeleteAvailability);
    public const string UpdateTimeSlotRoute = ControllerRoute + nameof(UpdateAvailability);


    [HttpPost]
    [Route(CreateTimeSlotRoute)]
    public ActionResult<AvailabilityResponseDto> CreateAvailability([FromBody] AvailabilityCreateRequestDto dto)
    {
        return Ok(availabilityService.CreateAvailability(dto));
    }
    
    [HttpDelete]
    [Route(DeleteTimeSlotRoute)]
    public ActionResult<AvailabilityResponseDto> DeleteAvailability(string id)
    {
        return Ok(availabilityService.DeleteAvailability(id));
    }
    
    [HttpPut]
    [Route(UpdateTimeSlotRoute)]
    public ActionResult<AvailabilityResponseDto> UpdateAvailability([FromBody] AvailabilityUpdateRequestDto dto)
    {
        return Ok(availabilityService.UpdateAvailability(dto));
    }
    
    
}