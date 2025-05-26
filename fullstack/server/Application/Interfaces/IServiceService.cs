using Application.Models.Dtos.Service;

namespace Application.Interfaces;

public interface IServiceService
{
    Task<ServiceResponseDto> CreateService(ServiceCreateRequestDto dto);

    Task<ServiceResponseDto> DeleteService(string id);
    
    Task<ServiceResponseDto> UpdateService(ServiceUpdateRequestDto dto);
    
    Task<List<ServiceResponseDto>> GetAllServices();
    
    Task<ServiceResponseDto> GetServiceById(string id);
}