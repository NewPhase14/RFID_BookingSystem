using Application.Models.Dtos.Availability;

namespace Application.Interfaces;

public interface IAvailabilityService
{
    public AvailabilityResponseDto CreateAvailability(AvailabilityCreateRequestDto dto);
    
    public AvailabilityResponseDto DeleteAvailability(string id);
    
    public AvailabilityResponseDto UpdateAvailability(AvailabilityUpdateRequestDto dto);
    
}