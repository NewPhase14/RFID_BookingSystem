using Application.Models.Dtos.Checking;

namespace Application.Interfaces.Infrastructure.Postgres;

public interface ICheckingRepository
{
    bool CheckBookingRequestDto(string rfid, string serviceId);
}