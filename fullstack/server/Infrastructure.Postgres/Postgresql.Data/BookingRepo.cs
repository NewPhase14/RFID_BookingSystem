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

    public void DeleteBooking(string id)
    {
        var booking = ctx.Bookings.FirstOrDefault(b => b.Id == id);
        if (booking == null)
        {
            throw new InvalidOperationException("Booking not found");
        }
        ctx.Bookings.Remove(booking);
        ctx.SaveChanges();
    }
}