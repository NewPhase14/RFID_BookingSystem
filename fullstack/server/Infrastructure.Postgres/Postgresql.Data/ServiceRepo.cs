using Application.Interfaces.Infrastructure.Postgres;
using Core.Domain.Entities;
using Infrastructure.Postgres.Scaffolding;

namespace Infrastructure.Postgres.Postgresql.Data;

public class ServiceRepo(MyDbContext ctx) : IServiceRepository
{
    public void AddService(Service service)
    {
        ctx.Services.Add(service);
        ctx.SaveChanges();
    }

    public void DeleteService(string id)
    {
        var service = ctx.Services.FirstOrDefault(s => s.Id == id);
        if (service == null)
        {
            throw new InvalidOperationException("Service not found");
        }
        ctx.Services.Remove(service);
        ctx.SaveChanges();
    }
}