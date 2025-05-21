using Core.Domain.Entities;

namespace Application.Interfaces.Infrastructure.Postgres;

public interface IBookingDataRepository
{
    Booking CreateBooking(Booking booking);

    Booking DeleteBooking(string id);
    
    bool BookingOverlapping(Booking booking);
}