using Application.Models.Dtos.Service;

namespace Application.Interfaces;

public interface IServiceService
{
    public ServiceResponseDto CreateService(ServiceCreateRequestDto dto);

    public ServiceResponseDto DeleteService(string id);
    
    public ServiceResponseDto UpdateService(ServiceUpdateRequestDto dto);
}