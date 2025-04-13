using Application.Interfaces;
using Application.Models.Dtos.Booking;
using Microsoft.AspNetCore.Mvc;

namespace Api.Rest.Controllers;

[ApiController]
public class BookingController(IBookingService bookingService) : ControllerBase
{
    public const string ControllerRoute = "api/booking/";
    
    public const string CreateBookingRoute = ControllerRoute + nameof(CreateBooking);
    
    [HttpPost]
    [Route(CreateBookingRoute)]
    public ActionResult<BookingResponseDto> CreateBooking([FromBody] BookingCreateRequestDto dto)
    {
        return Ok(bookingService.CreateBooking(dto));
    }
}
