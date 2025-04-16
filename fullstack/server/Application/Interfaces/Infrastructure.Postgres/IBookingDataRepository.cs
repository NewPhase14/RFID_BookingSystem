using Core.Domain.Entities;

namespace Application.Interfaces.Infrastructure.Postgres;

public interface IBookingDataRepository
{
    void AddBooking(Booking booking);

    void DeleteBooking(string id);
    
    bool BookingOverlapping(Booking booking);
}