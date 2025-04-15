using Application.Interfaces.Infrastructure.Postgres;
using Core.Domain.Entities;
using Infrastructure.Postgres.Scaffolding;

namespace Infrastructure.Postgres.Postgresql.Data;

public class TimeSlotRepo(MyDbContext ctx) : ITimeSlotRepository 
{
    public ServiceTimeSlot AddTimeSlot(ServiceTimeSlot serviceTimeSlot)
    {
        ctx.ServiceTimeSlots.Add(serviceTimeSlot);
        ctx.SaveChanges();
        return serviceTimeSlot;
    }
}