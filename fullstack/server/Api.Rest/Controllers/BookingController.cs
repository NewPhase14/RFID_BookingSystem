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
    
    public const string GetTodaysBookingsByUserIdRoute = ControllerRoute + nameof(GetTodaysBookingsByUserId);
    
    public const string GetFutureBookingsByUserIdRoute = ControllerRoute + nameof(GetFutureBookingsByUserId);
    
    public const string GetPastBookingsByUserIdRoute = ControllerRoute + nameof(GetPastBookingsByUserId);

    
    [HttpPost]
    [Route(CreateBookingRoute)]
    public async Task<ActionResult<BookingResponseDto>> CreateBooking([FromBody] BookingCreateRequestDto dto, [FromHeader] string authorization)
    {
        var jwt = security.VerifyJwtOrThrow(authorization);
        if (jwt.Role != "User")
        {
            return Unauthorized();
        }
        return Ok(await bookingService.CreateBooking(dto));
    }
    [HttpGet]
    [Route(GetAllBookingsRoute)]
    public async Task<ActionResult<List<BookingResponseDto>>> GetAllBookings([FromHeader] string authorization)
    {
        var jwt = security.VerifyJwtOrThrow(authorization);
        if (jwt.Role != "Admin")
        {
            return Unauthorized();
        }
        return Ok(await bookingService.GetAllBookings());
    }
    
    [HttpGet]
    [Route(GetLatestBookingsRoute)]
    public async Task<ActionResult<List<BookingResponseDto>>> GetLatestBookings([FromHeader] string authorization)
    {
        var jwt = security.VerifyJwtOrThrow(authorization);
        if (jwt.Role != "Admin")
        {
            return Unauthorized();
        }
        return Ok(await bookingService.GetLatestBookings());
    }
    
    [HttpDelete]
    [Route(DeleteBookingRoute)]
    public async Task<ActionResult<BookingResponseDto>> DeleteBooking(string id, [FromHeader] string authorization)
    {
        var jwt = security.VerifyJwtOrThrow(authorization);
        if (jwt.Role!= "User")
        {
            return Unauthorized();
        }
        return Ok(await bookingService.DeleteBooking(id));
    }
    
    [HttpGet]
    [Route(GetTodaysBookingsByUserIdRoute)]
    public async Task<ActionResult<List<BookingResponseDto>>> GetTodaysBookingsByUserId(string userId, [FromHeader] string authorization)
    {
        var jwt = security.VerifyJwtOrThrow(authorization);
        if (jwt.Role != "User")
        {
            return Unauthorized();
        }
        return Ok(await bookingService.GetTodaysBookingsByUserId(userId));
    }
    
    [HttpGet]
    [Route(GetFutureBookingsByUserIdRoute)]
    public async Task<ActionResult<List<BookingResponseDto>>> GetFutureBookingsByUserId(string userId, [FromHeader] string authorization)
    {
        var jwt = security.VerifyJwtOrThrow(authorization);
        if (jwt.Role != "User")
        {
            return Unauthorized();
        }
        return Ok(await bookingService.GetFutureBookingsByUserId(userId));
    }
    
    [HttpGet]
    [Route(GetPastBookingsByUserIdRoute)]
    public async Task<ActionResult<List<BookingResponseDto>>> GetPastBookingsByUserId(string userId, [FromHeader] string authorization)
    {
        var jwt = security.VerifyJwtOrThrow(authorization);
        if (jwt.Role != "User")
        {
            return Unauthorized();
        }
        return Ok(await bookingService.GetPastBookingsByUserId(userId));
    }
}
