using Application.Models.Dtos.Checking;

namespace Application.Interfaces;

public interface ICheckingService
{
    public Task<CheckingResponseDto> CheckIfValid(CheckBookingRequestDto dto);
}