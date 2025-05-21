using Application.Interfaces.Infrastructure.Postgres;
using Application.Services;
using Core.Domain.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Postgres.Postgresql.Data;

public class AvailabilityRepo(MyDbContext ctx) : IAvailabilityRepository 
{
    public ServiceAvailability CreateAvailability(ServiceAvailability availability)
    {
        var addedAvailability = ctx.ServiceAvailabilities.Add(availability);
        ctx.SaveChanges();
        return addedAvailability.Entity;
    }

    public List<ServiceAvailability> CreateAllAvailabilities(List<ServiceAvailability> availabilities)
    {
        ctx.ServiceAvailabilities.AddRange(availabilities);
        ctx.SaveChanges();
        return availabilities;
    }

    public ServiceAvailability GetAvailability(Booking booking, int bookingDay)
    {
        return ctx.ServiceAvailabilities
            .FirstOrDefault(sa => sa.ServiceId == booking.ServiceId && sa.DayOfWeek == bookingDay) ?? throw new InvalidOperationException();
    }

    public ServiceAvailability UpdateAvailability(ServiceAvailability availability)
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
        var updatedAvailability = ctx.ServiceAvailabilities.Update(existingAvailability);
        ctx.SaveChanges();
        return updatedAvailability.Entity;
    }

    public ServiceAvailability DeleteAvailability(string id)
    {
        var availability = ctx.ServiceAvailabilities.FirstOrDefault(a => a.Id == id);
        if (availability == null)
        {
            throw new InvalidOperationException("Availability not found");
        }
        var deletedAvailability = ctx.ServiceAvailabilities.Remove(availability);
        ctx.SaveChanges();
        return deletedAvailability.Entity;
    }
}