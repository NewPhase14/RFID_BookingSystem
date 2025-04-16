using Application.Interfaces.Infrastructure.Postgres;
using Application.Services;
using Core.Domain.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Postgres.Postgresql.Data;

public class AvailabilityRepo(MyDbContext ctx) : IAvailabilityRepository 
{
    public void AddAvailability(ServiceAvailability availability)
    {
        ctx.ServiceAvailabilities.Add(availability);
        ctx.SaveChanges();
    }

    public ServiceAvailability GetAvailability(Booking booking, int bookingDay)
    {
        return ctx.ServiceAvailabilities
            .FirstOrDefault(sa => sa.ServiceId == booking.ServiceId && sa.DayOfWeek == bookingDay) ?? throw new InvalidOperationException();
    }
}