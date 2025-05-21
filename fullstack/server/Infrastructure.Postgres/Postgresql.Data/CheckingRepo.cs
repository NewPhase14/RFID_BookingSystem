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
        var userId = string.Empty;
        var status = string.Empty;

        var user = ctx.Users.Include(user => user.Bookings)
            .FirstOrDefault(u => u.Rfid == rfid);

        if (user == null)
        {
            // throw exception 
            throw new InvalidOperationException("User not found");
        }
        
        foreach (Booking booking in user.Bookings)
        {
            if (booking.ServiceId == serviceId)
            {
                DateTime today = DateTime.Now;
                if (booking.Date != DateOnly.FromDateTime(today))
                {
                    throw new InvalidOperationException("Booking date is not today");
                }
                {
                    
                }
                if (booking.StartTime <= TimeOnly.FromDateTime(today) && booking.EndTime >= TimeOnly.FromDateTime(today))
                {
                    isValid = true;
                    userId = booking.UserId;
                    status = "success";
                }
                else
                {
                    isValid = false;
                    userId = booking.UserId;
                    status = "failed";
                }
            }
        }

        var activity = new CheckingBookingResponseDto
        {
            IsValid = isValid,
            ServiceId = serviceId,
            UserId = userId,
            Status = status
        };
        
        return activity;
    }
}