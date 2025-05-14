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
            Name = dto.Name,
            Description = dto.Description,
            ImageUrl = dto.ImageUrl
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

    public ServiceResponseDto UpdateService(ServiceUpdateRequestDto dto)
    {
        var service = new Service()
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            ImageUrl = dto.ImageUrl
        };
        serviceRepository.UpdateService(service);
        return new ServiceResponseDto()
        {
            Message = "Service updated successfully"
        };
    }

    public List<GetAllServiceResponseDto> GetAllServices()
    {
        var services = serviceRepository.GetAllServices();
        
        return services.Select(service => new GetAllServiceResponseDto
        {
            Id = service.Id,
            Name = service.Name,
            Description = service.Description,
            ImageUrl = service.ImageUrl
        }).ToList();
        
    }
}