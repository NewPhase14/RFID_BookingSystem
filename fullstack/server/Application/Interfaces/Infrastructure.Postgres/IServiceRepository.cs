using Core.Domain.Entities;

namespace Application.Interfaces.Infrastructure.Postgres;

public interface IServiceRepository
{
    void AddService(Service service);
    void DeleteService(string id);
    void UpdateService(Service service);
    List<Service> GetAllServices();
    Service GetServiceById(string id);
}