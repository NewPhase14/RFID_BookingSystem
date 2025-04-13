using Core.Domain.Entities;

namespace Application.Interfaces.Infrastructure.Postgres;

public interface IBookingDataRepository
{
    Booking AddBooking(Booking booking);
}