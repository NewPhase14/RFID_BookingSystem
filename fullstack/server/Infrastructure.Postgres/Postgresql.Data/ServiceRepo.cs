using Application.Interfaces.Infrastructure.Postgres;
using Core.Domain.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Postgres.Postgresql.Data;

public class ServiceRepo(MyDbContext ctx) : IServiceRepository
{
    public Service CreateService(Service service)
    {
        var createdService = ctx.Services.Add(service);
        ctx.SaveChanges();
        return createdService.Entity;
    }

    public Service DeleteService(string id)
    {
        var service = ctx.Services.FirstOrDefault(s => s.Id == id);
        if (service == null)
        {
            throw new InvalidOperationException("Service not found");
        }
        ctx.Services.Remove(service);
        ctx.SaveChanges();
        
        return service;
    }

    public Service UpdateService(Service service)
    {
        var existingService = ctx.Services.FirstOrDefault(s => s.Id == service.Id);
        if (existingService == null)
        {
            throw new InvalidOperationException("Service not found");
        }
        existingService.Name = service.Name;
        existingService.Description = service.Description;
        existingService.ImageUrl = service.ImageUrl;
        existingService.UpdatedAt = DateTime.Now;
        var updatedService = ctx.Services.Update(existingService);
        ctx.SaveChanges();
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

    public Service GetServiceById(string id)
    {
        var service = ctx.Services.FirstOrDefault(s => s.Id == id);
        if (service == null)
        {
            throw new InvalidOperationException("Service not found");
        }
        return service;
    }
}