using Application.Interfaces.Infrastructure.Postgres;
using Application.Models.Dtos.Checking;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Postgres.Postgresql.Data;

public class CheckingRepo(MyDbContext ctx) : ICheckingRepository
{
    public async Task<CheckingBookingResponseDto> CheckBookingRequestDto(string rfid, string serviceId)
    {
        
        var europeanTime = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
        var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, europeanTime);
        
        var isValid = false;
        var status = "failed";

        var user = await ctx.Users.Include(user => user.Bookings)
            .FirstOrDefaultAsync(u => u.Rfid == rfid);

        if (user == null) throw new InvalidOperationException("User not found");

        foreach (var booking in user.Bookings)
            if (booking.ServiceId == serviceId)
            {
                if (booking.StartTime > TimeOnly.FromDateTime(now) ||
                    booking.EndTime < TimeOnly.FromDateTime(now) ||
                    booking.Date != DateOnly.FromDateTime(now)) continue;
                isValid = true;
                status = "success";
            }

        var activity = new CheckingBookingResponseDto
        {
            IsValid = isValid,
            ServiceId = serviceId,
            UserId = user.Id,
            Status = status
        };

        return activity;
    }
}