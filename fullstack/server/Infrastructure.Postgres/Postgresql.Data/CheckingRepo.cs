using Application.Interfaces.Infrastructure.Postgres;
using Core.Domain.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Postgres.Postgresql.Data;

public class CheckingRepo(MyDbContext ctx) : ICheckingRepository
{
    public bool CheckBookingRequestDto(string rfid, string serviceId)
    {
        bool isValid = false;

        var user = ctx.Users.Include(user => user.Bookings)
            .FirstOrDefault(u => u.Rfid == rfid);

        if (user == null)
        {
            return isValid;
        }
        
        foreach (Booking booking in user.Bookings)
        {
            if (booking.ServiceId == serviceId)
            {
                if (booking.StartTime <= DateTime.Now && booking.EndTime >= DateTime.Now)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
        }
        return isValid;
    }
}