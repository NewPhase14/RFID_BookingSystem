using Application.Models.Dtos.Checking;

namespace Application.Interfaces.Infrastructure.Postgres;

public interface ICheckingRepository
{
    CheckingBookingResponseDto CheckBookingRequestDto(string rfid, string serviceId);
}