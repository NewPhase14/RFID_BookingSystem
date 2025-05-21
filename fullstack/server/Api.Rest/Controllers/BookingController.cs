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
    
    public const string GetAllBookingsRoute = ControllerRoute + nameof(GetAllBookings);
    
    public const string GetLatestBookingsRoute = ControllerRoute + nameof(GetLatestBookings);
    
    public const string DeleteBookingRoute = ControllerRoute + nameof(DeleteBooking);
    
    public const string GetAvailabilitySlotsRoute = ControllerRoute + nameof(GetAvailabilitySlots);
    
    public const string GetTodaysBookingsByUserIdRoute = ControllerRoute + nameof(GetTodaysBookingsByUserId);
    
    public const string GetFutureBookingsByUserIdRoute = ControllerRoute + nameof(GetFutureBookingsByUserId);
    
    public const string GetPastBookingsByUserIdRoute = ControllerRoute + nameof(GetPastBookingsByUserId);

    
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
    [HttpGet]
    [Route(GetAllBookingsRoute)]
    public ActionResult<List<BookingResponseDto>> GetAllBookings([FromHeader] string authorization)
    {
        var jwt = security.VerifyJwtOrThrow(authorization);
        if (jwt.Role != "Admin")
        {
            return Unauthorized();
        }
        return Ok(bookingService.GetAllBookings());
    }
    
    [HttpGet]
    [Route(GetLatestBookingsRoute)]
    public ActionResult<List<BookingResponseDto>> GetLatestBookings([FromHeader] string authorization)
    {
        var jwt = security.VerifyJwtOrThrow(authorization);
        if (jwt.Role != "Admin")
        {
            return Unauthorized();
        }
        return Ok(bookingService.GetLatestBookings());
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
    
    [HttpGet]
    [Route(GetTodaysBookingsByUserIdRoute)]
    public ActionResult<List<BookingResponseDto>> GetTodaysBookingsByUserId(string userId)
    {
        
        return Ok(bookingService.GetTodaysBookingsByUserId(userId));
    }
    
    [HttpGet]
    [Route(GetFutureBookingsByUserIdRoute)]
    public ActionResult<List<BookingResponseDto>> GetFutureBookingsByUserId(string userId)
    {
        return Ok(bookingService.GetFutureBookingsByUserId(userId));
    }
    
    [HttpGet]
    [Route(GetPastBookingsByUserIdRoute)]
    public ActionResult<List<BookingResponseDto>> GetPastBookingsByUserId(string userId)
    {
        return Ok(bookingService.GetPastBookingsByUserId(userId));
    }
}
