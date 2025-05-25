using Application.Interfaces.Infrastructure.Postgres;
using Core.Domain.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Postgres.Postgresql.Data;

public class BookingRepo(MyDbContext ctx) : IBookingDataRepository
{
    public Booking CreateBooking(Booking booking)
    {
        var createdBooking = ctx.Bookings.Add(booking);
        ctx.SaveChanges();
        return createdBooking.Entity;
    }

    public List<Booking> GetAllBookings()
    {
        var bookings = ctx.Bookings
            .Include(b => b.Service)
            .Include(b => b.User)
            .OrderByDescending(b => b.CreatedAt)
            .ToList();
        return bookings;
    }

    public List<Booking> GetLatestBookings()
    {
        var bookings = ctx.Bookings
            .Include(b => b.Service)
            .Include(b => b.User)
            .OrderByDescending(b => b.CreatedAt)
            .Take(5)
            .ToList();

        return bookings;
    }

    public Booking DeleteBooking(string id)
    {
        var booking = ctx.Bookings
            .Include(b => b.Service)
            .Include(b => b.User)
            .FirstOrDefault(b => b.Id == id);
        if (booking == null)
        {
            throw new InvalidOperationException("Booking not found");
        }
        ctx.Bookings.Remove(booking);
        ctx.SaveChanges();
        return booking;
    }

    public bool BookingOverlapping(Booking booking)
    {
        return  ctx.Bookings.Any(b =>
            b.ServiceId == booking.ServiceId &&
            b.StartTime < booking.EndTime &&
            booking.StartTime < b.EndTime && b.Date == booking.Date
        );
    }

    public List<Booking> GetFutureBookingsByUserId(string userId)
    {
        var now = DateTime.Now;
        var today = DateOnly.FromDateTime(now);
        var currentTime = TimeOnly.FromDateTime(now);

        var bookings = ctx.Bookings
            .Include(b => b.Service)
            .Include(b => b.User)
            .Where(b => b.UserId == userId &&
                        (b.Date > today || (b.Date == today && b.StartTime > currentTime)))
            .OrderBy(b => b.Date)
            .ThenBy(b => b.StartTime)
            .ToList();

        return bookings;
    }

    public List<Booking> GetPastBookingsByUserId(string userId)
    {
        var now = DateTime.Now;
        var today = DateOnly.FromDateTime(now);
        var currentTime = TimeOnly.FromDateTime(now);

        var bookings = ctx.Bookings
            .Include(b => b.Service)
            .Include(b => b.User)
            .Where(b => b.UserId == userId &&
                        (b.Date < today || (b.Date == today && b.EndTime < currentTime)))
            .OrderByDescending(b => b.Date)
            .ThenByDescending(b => b.StartTime)
            .ToList();

        return bookings;
    }


    public List<Booking> GetTodaysBookingsByUserId(string userId)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);

        var bookings = ctx.Bookings
            .Include(b => b.Service)
            .Include(b => b.User)
            .Where(b => b.UserId == userId && b.Date == today)
            .OrderBy(b => b.StartTime)
            .ToList();

        return bookings;
    }
    
       
}