using Application.Models.Dtos.Checking;

namespace Application.Interfaces;

public interface ICheckingService
{
    public CheckingResponseDto CheckIfValid(CheckBookingRequestDto dto);
}