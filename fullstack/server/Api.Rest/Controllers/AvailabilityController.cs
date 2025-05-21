using Application.Interfaces;
using Application.Models.Dtos;
using Application.Models.Dtos.Availability;
using Microsoft.AspNetCore.Mvc;

namespace Api.Rest.Controllers;

[ApiController]

public class AvailabilityController(IAvailabilityService availabilityService) : ControllerBase
{
    public const string ControllerRoute = "api/availability/";
    
    public const string CreateAvailabilityRoute = ControllerRoute + nameof(CreateAvailability);
    
    public const string CreateAllAvailabilitiesRoute = ControllerRoute + nameof(CreateAllAvailabilities);
    
    public const string DeleteAvailabilityRoute = ControllerRoute + nameof(DeleteAvailability);
    
    public const string UpdateAvailabilityRoute = ControllerRoute + nameof(UpdateAvailability);


    [HttpPost]
    [Route(CreateAvailabilityRoute)]
    public ActionResult<AvailabilityResponseDto> CreateAvailability([FromBody] AvailabilityCreateRequestDto dto)
    {
        return Ok(availabilityService.CreateAvailability(dto));
    }
    
    [HttpPost]
    [Route(CreateAllAvailabilitiesRoute)]
    public ActionResult<List<AvailabilityResponseDto>> CreateAllAvailabilities([FromBody] List<AvailabilityCreateRequestDto> dtos)
    {
        return Ok(availabilityService.CreateAllAvailabilities(dtos));
    }
    
    
    [HttpDelete]
    [Route(DeleteAvailabilityRoute)]
    public ActionResult<AvailabilityResponseDto> DeleteAvailability(string id)
    {
        return Ok(availabilityService.DeleteAvailability(id));
    }
    
    [HttpPut]
    [Route(UpdateAvailabilityRoute)]
    public ActionResult<AvailabilityResponseDto> UpdateAvailability([FromBody] AvailabilityUpdateRequestDto dto)
    {
        return Ok(availabilityService.UpdateAvailability(dto));
    }
    
    
}