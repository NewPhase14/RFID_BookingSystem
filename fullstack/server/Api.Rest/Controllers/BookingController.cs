using Application.Interfaces;
using Application.Models.Dtos.Availability;
using Application.Models.Dtos.Booking;
using Microsoft.AspNetCore.Mvc;

namespace Api.Rest.Controllers;

[ApiController]
public class BookingController(IBookingService bookingService, ISecurityService security) : ControllerBase
{
    public const string ControllerRoute = "api/booking/";
    
    public const string CreateBookingRoute = ControllerRoute + nameof(CreateBooking);
    
    public const string DeleteBookingRoute = ControllerRoute + nameof(DeleteBooking);
    
    public const string GetAvailabilitySlotsRoute = ControllerRoute + nameof(GetAvailabilitySlots);

    
    [HttpPost]
    [Route(CreateBookingRoute)]
    public ActionResult<BookingResponseDto> CreateBooking([FromBody] BookingCreateRequestDto dto, [FromHeader] string authorization)
    {
        var jwt = security.VerifyJwtOrThrow(authorization);
        if (jwt.Role != "User")
        {
            return Unauthorized();
        }
        return Ok(bookingService.CreateBooking(dto));
    }
    
    [HttpDelete]
    [Route(DeleteBookingRoute)]
    public ActionResult<BookingResponseDto> DeleteBooking(string id, [FromHeader] string authorization)
    {
        var jwt = security.VerifyJwtOrThrow(authorization);
        if (jwt.Role!= "User")
        {
            return Unauthorized();
        }
        return Ok(bookingService.DeleteBooking(id));
    }
    
    [HttpGet]
    [Route(GetAvailabilitySlotsRoute)]
    public ActionResult<List<AvailabiltySlotsDto>> GetAvailabilitySlots(string serviceId)
    {
        return Ok(bookingService.GetAvailabilitySlots(serviceId));
    }
}
