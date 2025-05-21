using Application.Interfaces.Infrastructure.Postgres;
using Application.Models.Dtos.Checking;
using Core.Domain.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Postgres.Postgresql.Data;

public class CheckingRepo(MyDbContext ctx) : ICheckingRepository
{
    public CheckingBookingResponseDto CheckBookingRequestDto(string rfid, string serviceId)
    {
        bool isValid = false;
        var status = "failed";

        var user = ctx.Users.Include(user => user.Bookings)
            .FirstOrDefault(u => u.Rfid == rfid);

        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }
        
        foreach (Booking booking in user.Bookings)
        {
            if (booking.ServiceId == serviceId)
            {
                DateTime today = DateTime.Now;
                if (booking.StartTime > TimeOnly.FromDateTime(today) ||
                    booking.EndTime < TimeOnly.FromDateTime(today) ||
                    booking.Date != DateOnly.FromDateTime(today)) continue;
                isValid = true;
                status = "success";
            }
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