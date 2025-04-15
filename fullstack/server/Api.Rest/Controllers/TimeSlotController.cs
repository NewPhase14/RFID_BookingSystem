using Application.Interfaces;
using Application.Models.Dtos;
using Application.Models.Dtos.TimeSlot;
using Microsoft.AspNetCore.Mvc;

namespace Api.Rest.Controllers;

[ApiController]

public class TimeSlotController(ITimeSlotService timeSlotService) : ControllerBase
{
    public const string ControllerRoute = "api/timeSlot/";
    
    public const string CreateTimeSlotRoute = ControllerRoute + nameof(CreateTimeSlot);

    [HttpPost]
    [Route(CreateTimeSlotRoute)]
    public ActionResult<AuthResponseDto> CreateTimeSlot([FromBody] TimeSlotCreateRequestDto dto)
    {
        return Ok(timeSlotService.CreateTimeSlot(dto));
    }
    
}