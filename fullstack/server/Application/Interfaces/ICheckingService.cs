using Application.Models.Dtos.ActivityLog;
using Application.Models.Dtos.Checking;
using Core.Domain.Entities;

namespace Application.Interfaces;

public interface ICheckingService
{
    public Task<CheckingResponseDto> CheckIfValid(CheckBookingRequestDto dto);
}