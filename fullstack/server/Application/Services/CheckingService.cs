using Application.Interfaces;
using Application.Interfaces.Infrastructure.Postgres;
using Application.Models.Dtos.Checking;

namespace Application.Services;

public class CheckingService(ICheckingRepository checkingRepository) : ICheckingService
{
    public CheckingResponseDto CheckIfValid(CheckBookingRequestDto dto)
    {
        var isValid = checkingRepository.CheckBookingRequestDto(dto.Rfid, dto.ServiceId);
        return new CheckingResponseDto
        {
            Message = isValid ? "Valid" : "Invalid"
        };
    }
}