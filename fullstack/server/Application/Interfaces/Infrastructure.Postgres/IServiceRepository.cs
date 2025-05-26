using Core.Domain.Entities;

namespace Application.Interfaces.Infrastructure.Postgres;

public interface IServiceRepository
{
    Service CreateService(Service service);
    Service DeleteService(string id);
    Service UpdateService(Service service);
    Task<List<Service>> GetAllServices();
    Service GetServiceById(string id);
}