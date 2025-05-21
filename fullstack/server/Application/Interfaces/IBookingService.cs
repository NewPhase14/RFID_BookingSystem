using Application.Models.Dtos.Availability;
using Application.Models.Dtos.Booking;

namespace Application.Interfaces;

public interface IBookingService
{
    public Task<BookingResponseDto> CreateBooking(BookingCreateRequestDto dto);
    
    public List<BookingResponseDto> GetAllBookings();
    
    public List<BookingResponseDto> GetLatestBookings();

    public BookingResponseDto DeleteBooking(string id);
    
    public List<AvailabiltySlotsDto> GetAvailabilitySlots(string serviceId);
    
    List<BookingResponseDto> GetTodaysBookingsByUserId(string userId);
    List<BookingResponseDto> GetFutureBookingsByUserId(string userId);
    List<BookingResponseDto> GetPastBookingsByUserId(string userId);
    
}