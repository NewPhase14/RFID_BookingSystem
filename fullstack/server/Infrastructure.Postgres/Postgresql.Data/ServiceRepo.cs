using Application.Interfaces.Infrastructure.Postgres;
using Core.Domain.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Postgres.Postgresql.Data;

public class ServiceRepo(MyDbContext ctx) : IServiceRepository
{
    public async Task<Service> CreateService(Service service)
    {
        var createdService = await ctx.Services.AddAsync(service);
        await ctx.SaveChangesAsync();
        return createdService.Entity;
    }

    public async Task<Service> DeleteService(string id)
    {
        var service = await ctx.Services.FirstOrDefaultAsync(s => s.Id == id);
        if (service == null)
        {
            throw new InvalidOperationException("Service not found");
        }
        ctx.Services.Remove(service);
        await ctx.SaveChangesAsync();
        
        return service;
    }

    public async Task<Service> UpdateService(Service service)
    {
        var existingService = await ctx.Services.FirstOrDefaultAsync(s => s.Id == service.Id);
        if (existingService == null)
        {
            throw new InvalidOperationException("Service not found");
        }
        existingService.Name = service.Name;
        existingService.Description = service.Description;
        existingService.ImageUrl = service.ImageUrl;
        existingService.UpdatedAt = DateTime.Now;
        var updatedService = ctx.Services.Update(existingService);
        await ctx.SaveChangesAsync();
        return updatedService.Entity;
    }

    public async Task<List<Service>> GetAllServices()
    {
        var services = await ctx.Services.ToListAsync();
        if (services == null || services.Count == 0)
        {
            throw new InvalidOperationException("No services found");
        }
        return services;
    }

    public async Task<Service> GetServiceById(string id)
    {
        var service = await ctx.Services.FirstOrDefaultAsync(s => s.Id == id);
        if (service == null)
        {
            throw new InvalidOperationException("Service not found");
        }
        return service;
    }
}