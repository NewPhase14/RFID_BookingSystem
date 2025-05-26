using System.Drawing.Printing;
using Application.Interfaces.Infrastructure.Postgres;
using Core.Domain.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Postgres.Postgresql.Data;

public class BookingRepo(MyDbContext ctx) : IBookingRepository
{
    public async Task<Booking> CreateBooking(Booking booking)
    {
        await ctx.Bookings.AddAsync(booking);
        await ctx.SaveChangesAsync();
        return booking;
    }

    public async Task<List<Booking>> GetAllBookings()
    {
        var bookings = await ctx.Bookings
            .Include(b => b.Service)
            .Include(b => b.User)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();
        return bookings;
    }

    public async Task<List<Booking>> GetLatestBookings()
    {
        var bookings = await ctx.Bookings
            .Include(b => b.Service)
            .Include(b => b.User)
            .OrderByDescending(b => b.CreatedAt)
            .Take(5)
            .ToListAsync();
        return bookings;
    }

    public async Task<Booking> DeleteBooking(string id)
    {
        var booking = await ctx.Bookings
            .Include(b => b.Service)
            .Include(b => b.User)
            .FirstOrDefaultAsync(b => b.Id == id);
        if (booking == null)
        {
            throw new InvalidOperationException("Booking not found");
        }
        ctx.Bookings.Remove(booking);
        await ctx.SaveChangesAsync();
        return booking;
    }

    public async Task<bool> BookingOverlapping(Booking booking)
    {
        return await ctx.Bookings.AnyAsync(b =>
            b.ServiceId == booking.ServiceId &&
            b.StartTime < booking.EndTime &&
            booking.StartTime < b.EndTime && b.Date == booking.Date
        );
    }

    public async Task<List<Booking>> GetFutureBookingsByUserId(string userId)
    {
        var now = DateTime.Now;
        var today = DateOnly.FromDateTime(now);
        var currentTime = TimeOnly.FromDateTime(now);

        var bookings = await ctx.Bookings
            .Include(b => b.Service)
            .Include(b => b.User)
            .Where(b => b.UserId == userId &&
                        (b.Date > today || (b.Date == today && b.StartTime > currentTime)))
            .OrderBy(b => b.Date)
            .ThenBy(b => b.StartTime)
            .ToListAsync();

        return bookings;
    }

    public async Task<List<Booking>> GetPastBookingsByUserId(string userId)
    {
        var now = DateTime.Now;
        var today = DateOnly.FromDateTime(now);
        var currentTime = TimeOnly.FromDateTime(now);

        var bookings = await ctx.Bookings
            .Include(b => b.Service)
            .Include(b => b.User)
            .Where(b => b.UserId == userId &&
                        (b.Date < today || (b.Date == today && b.EndTime <= currentTime)))
            .OrderByDescending(b => b.Date)
            .ThenByDescending(b => b.StartTime)
            .ToListAsync();

        return bookings;
    }


    public async Task<List<Booking>> GetTodaysBookingsByUserId(string userId)
    {
        var now = DateTime.Now;
        var today = DateOnly.FromDateTime(now);
        var currentTime = TimeOnly.FromDateTime(now);

        var bookings = await ctx.Bookings
            .Include(b => b.Service)
            .Include(b => b.User)
            .Where(b => b.UserId == userId && b.Date == today && b.EndTime > currentTime)
            .OrderBy(b => b.StartTime)
            .ToListAsync();

        return bookings;
    }

    public async Task<List<Booking>> GetBookingsForServiceAndDate(string serviceId, DateOnly date)
    {
        var bookings = await ctx.Bookings.
            Where(b => b.ServiceId == serviceId && b.Date == date)
            .ToListAsync();
        
        return bookings;
    }
}