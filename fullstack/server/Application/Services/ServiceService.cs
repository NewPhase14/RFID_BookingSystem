using Application.Interfaces;
using Application.Interfaces.Infrastructure.Postgres;
using Application.Models.Dtos.Service;
using Core.Domain.Entities;

namespace Application.Services;

public class ServiceService(IServiceRepository serviceRepository) : IServiceService
{
    public ServiceResponseDto CreateService(ServiceCreateRequestDto dto)
    {
        var service = new Service()
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name
        };
        serviceRepository.AddService(service);
        return new ServiceResponseDto()
        {
            Message = "Service created successfully"
        };
    }

    public ServiceResponseDto DeleteService(string id)
    {
        serviceRepository.DeleteService(id);
        return new ServiceResponseDto()
        {
            Message = "Service deleted successfully"
        };
    }

    public ServiceResponseDto UpdateService(string id)
    {
        // TODO: Implement update service logic
        throw new NotImplementedException("Update service logic is not implemented yet.");
    }
}