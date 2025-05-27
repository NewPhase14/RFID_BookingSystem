using Application.Models.Dtos.Booking;

namespace Application.Interfaces;

public interface IBookingService
{
    Task<BookingResponseDto> CreateBooking(BookingCreateRequestDto dto);

    Task<List<BookingResponseDto>> GetAllBookings();

    Task<List<BookingResponseDto>> GetLatestBookings();

    Task<BookingResponseDto> DeleteBooking(string id);

    Task<List<BookingResponseDto>> GetTodaysBookingsByUserId(string userId);

    Task<List<BookingResponseDto>> GetFutureBookingsByUserId(string userId);

    Task<List<BookingResponseDto>> GetPastBookingsByUserId(string userId);
}