using Application.Models.Dtos.TimeSlot;

namespace Application.Interfaces;

public interface ITimeSlotService
{
    public TimeSlotResponseDto CreateTimeSlot(TimeSlotCreateRequestDto dto);
    
}