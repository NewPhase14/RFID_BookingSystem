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

    public void UpdateAvailability(ServiceAvailability availability)
    {
        var existingAvailability = ctx.ServiceAvailabilities
            .FirstOrDefault(a => a.Id == availability.Id);
        if (existingAvailability == null)
        {
            throw new InvalidOperationException("Availability not found");
        }
        existingAvailability.UpdatedAt = DateTime.Now;
        existingAvailability.DayOfWeek = availability.DayOfWeek;
        existingAvailability.AvailableFrom = availability.AvailableFrom;
        existingAvailability.AvailableTo = availability.AvailableTo;
        ctx.ServiceAvailabilities.Update(existingAvailability);
        ctx.SaveChanges();
    }

    public void DeleteAvailability(string id)
    {
        var availability = ctx.ServiceAvailabilities.FirstOrDefault(a => a.Id == id);
        if (availability == null)
        {
            throw new InvalidOperationException("Availability not found");
        }
        ctx.ServiceAvailabilities.Remove(availability);
        ctx.SaveChanges();
    }
}