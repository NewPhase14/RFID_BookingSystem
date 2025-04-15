using Core.Domain.Entities;

namespace Application.Interfaces.Infrastructure.Postgres;

public interface ITimeSlotRepository
{
    ServiceTimeSlot AddTimeSlot(ServiceTimeSlot serviceTimeSlot);
}