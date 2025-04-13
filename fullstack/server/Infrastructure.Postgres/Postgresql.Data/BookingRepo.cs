using Application.Interfaces.Infrastructure.Postgres;
using Core.Domain.Entities;
using Infrastructure.Postgres.Scaffolding;

namespace Infrastructure.Postgres.Postgresql.Data;

public class BookingRepo(MyDbContext ctx) : IBookingDataRepository
{
    public Booking AddBooking(Booking booking)
    {
        ctx.Bookings.Add(booking);
        ctx.SaveChanges();
        return booking;
    }
}