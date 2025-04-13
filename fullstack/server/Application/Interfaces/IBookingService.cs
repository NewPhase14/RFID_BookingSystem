using Application.Models.Dtos.Booking;

namespace Application.Interfaces;

public interface IBookingService
{
    public BookingResponseDto CreateBooking(BookingCreateRequestDto dto);
}