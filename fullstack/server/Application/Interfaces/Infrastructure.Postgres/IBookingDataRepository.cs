using Core.Domain.Entities;

namespace Application.Interfaces.Infrastructure.Postgres;

public interface IBookingDataRepository
{
    Task<Booking> CreateBooking(Booking booking);
    
    Task<List<Booking>> GetAllBookings();
    
    Task<List<Booking>> GetLatestBookings();

    Task<Booking> DeleteBooking(string id);
    
    Task<bool> BookingOverlapping(Booking booking);
    
    Task<List<Booking>> GetFutureBookingsByUserId(string userId);
    
    Task<List<Booking>> GetPastBookingsByUserId(string userId);
    
    Task<List<Booking>> GetTodaysBookingsByUserId(string userId);

    Task<List<Booking>> GetBookingsForServiceAndDate(string serviceId, DateOnly date);
}