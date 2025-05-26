using Application.Interfaces;
using Application.Models.Dtos.Checking;
using Microsoft.AspNetCore.Mvc;

namespace Api.Rest.Controllers;

[ApiController]
public class CheckingController(ICheckingService checkingService) : ControllerBase
{
    public const string ControllerRoute = "api/checking/";
    
    public const string CheckBookingRequestRoute = ControllerRoute + nameof(CheckBookingRequest);
    
    [HttpPost]
    [Route(CheckBookingRequestRoute)]
    public async Task<ActionResult<CheckingResponseDto>> CheckBookingRequest([FromBody] CheckBookingRequestDto dto)
    {
        return Ok(await checkingService.CheckIfValid(dto));
    }
}