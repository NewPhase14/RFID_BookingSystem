using Application.Models.Dtos.Availability;

namespace Application.Interfaces;

public interface IAvailabilityService
{
    AvailabilityResponseDto CreateAvailability(AvailabilityCreateRequestDto dto);
    
    List<AvailabilityResponseDto> CreateAllAvailabilities(List<AvailabilityCreateRequestDto> dtos);
    
    AvailabilityResponseDto DeleteAvailability(string id);
    
    AvailabilityResponseDto UpdateAvailability(AvailabilityUpdateRequestDto dto);
    
    Task<List<AvailabiltySlotsDto>> GetAvailabilitySlots(string serviceId);

    
}