using Application.Models.Dtos.Availability;

namespace Application.Interfaces;

public interface IAvailabilityService
{
    Task<AvailabilityResponseDto> CreateAvailability(AvailabilityCreateRequestDto dto);

    Task<List<AvailabilityResponseDto>> CreateAllAvailabilities(List<AvailabilityCreateRequestDto> dtos);

    Task<AvailabilityResponseDto> DeleteAvailability(string id);

    Task<AvailabilityResponseDto> UpdateAvailability(AvailabilityUpdateRequestDto dto);

    Task<List<AvailabiltySlotsDto>> GetAvailabilitySlots(string serviceId);
}