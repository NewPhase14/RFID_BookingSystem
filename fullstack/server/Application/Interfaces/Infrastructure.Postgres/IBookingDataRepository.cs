using Core.Domain.Entities;

namespace Application.Interfaces.Infrastructure.Postgres;

public interface IBookingDataRepository
{
    Booking CreateBooking(Booking booking);
    
    List<Booking> GetAllBookings();
    
    List<Booking> GetLatestBookings();

    Booking DeleteBooking(string id);
    
    bool BookingOverlapping(Booking booking);
    
    List<Booking> GetFutureBookingsByUserId(string userId);
    
    List<Booking> GetPastBookingsByUserId(string userId);
    
    List<Booking> GetTodaysBookingsByUserId(string userId);
}