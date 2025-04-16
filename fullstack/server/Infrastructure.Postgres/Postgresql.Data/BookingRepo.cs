using Application.Interfaces.Infrastructure.Postgres;
using Core.Domain.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Postgres.Postgresql.Data;

public class BookingRepo(MyDbContext ctx) : IBookingDataRepository
{
    public void AddBooking(Booking booking)
    {
        ctx.Bookings.Add(booking);
        ctx.SaveChanges();
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

    public bool BookingOverlapping(Booking booking)
    {
        return  ctx.Bookings.Any(b =>
            b.ServiceId == booking.ServiceId &&
            b.StartTime < booking.EndTime &&
            booking.StartTime < b.EndTime
        );
    }
    
}