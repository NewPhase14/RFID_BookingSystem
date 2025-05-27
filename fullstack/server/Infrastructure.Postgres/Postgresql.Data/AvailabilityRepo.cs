using Application.Interfaces.Infrastructure.Postgres;
using Core.Domain.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Postgres.Postgresql.Data;

public class AvailabilityRepo(MyDbContext ctx) : IAvailabilityRepository
{
    public async Task<ServiceAvailability> CreateAvailability(ServiceAvailability availability)
    {
        var addedAvailability = ctx.ServiceAvailabilities.Add(availability);
        await ctx.SaveChangesAsync();
        return addedAvailability.Entity;
    }

    public async Task<List<ServiceAvailability>> CreateAllAvailabilities(List<ServiceAvailability> availabilities)
    {
        await ctx.ServiceAvailabilities.AddRangeAsync(availabilities);
        await ctx.SaveChangesAsync();
        return availabilities;
    }

    public async Task<ServiceAvailability> GetAvailability(Booking booking, int bookingDay)
    {
        return await ctx.ServiceAvailabilities
                   .FirstOrDefaultAsync(sa => sa.ServiceId == booking.ServiceId && sa.DayOfWeek == bookingDay) ??
               throw new InvalidOperationException();
    }

    public async Task<ServiceAvailability> UpdateAvailability(ServiceAvailability availability)
    {
        var europeanTime = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
        var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, europeanTime);
        
        var existingAvailability = await ctx.ServiceAvailabilities
            .FirstOrDefaultAsync(a => a.Id == availability.Id);
        if (existingAvailability == null) throw new InvalidOperationException("Availability not found");
        existingAvailability.UpdatedAt = now;
        existingAvailability.DayOfWeek = availability.DayOfWeek;
        existingAvailability.AvailableFrom = availability.AvailableFrom;
        existingAvailability.AvailableTo = availability.AvailableTo;
        var updatedAvailability = ctx.ServiceAvailabilities.Update(existingAvailability);
        await ctx.SaveChangesAsync();
        return updatedAvailability.Entity;
    }

    public async Task<ServiceAvailability> DeleteAvailability(string id)
    {
        var availability = await ctx.ServiceAvailabilities.FirstOrDefaultAsync(a => a.Id == id);
        if (availability == null) throw new InvalidOperationException("Availability not found");
        var deletedAvailability = ctx.ServiceAvailabilities.Remove(availability);
        await ctx.SaveChangesAsync();
        return deletedAvailability.Entity;
    }

    public async Task<ServiceAvailability> GetAvailabilityForServiceAndDay(string serviceId, int dayOfWeek)
    {
        return await ctx.ServiceAvailabilities
            .FirstOrDefaultAsync(a => a.ServiceId == serviceId && a.DayOfWeek == dayOfWeek);
    }
}