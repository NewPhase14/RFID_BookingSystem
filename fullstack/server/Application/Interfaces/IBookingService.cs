using Application.Models.Dtos.Availability;
using Application.Models.Dtos.Booking;

namespace Application.Interfaces;

public interface IBookingService
{
    public BookingResponseDto CreateBooking(BookingCreateRequestDto dto);

    public BookingResponseDto DeleteBooking(string id);
    
    public List<AvailabiltySlotsDto> GetAvailabilitySlots(string serviceId);
}