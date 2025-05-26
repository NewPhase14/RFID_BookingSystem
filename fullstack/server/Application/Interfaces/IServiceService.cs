using Application.Models.Dtos.Service;

namespace Application.Interfaces;

public interface IServiceService
{
    public Task<ServiceResponseDto> CreateService(ServiceCreateRequestDto dto);

    public ServiceResponseDto DeleteService(string id);
    
    public ServiceResponseDto UpdateService(ServiceUpdateRequestDto dto);
    
    public Task<List<ServiceResponseDto>> GetAllServices();
    
    public ServiceResponseDto GetServiceById(string id);
}