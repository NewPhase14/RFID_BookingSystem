using Core.Domain.Entities;

namespace Application.Interfaces.Infrastructure.Postgres;

public interface IServiceRepository
{
    Task<Service> CreateService(Service service);
    Task<Service> DeleteService(string id);
    Task<Service> UpdateService(Service service);
    Task<List<Service>> GetAllServices();
    Task<Service> GetServiceById(string id);
}