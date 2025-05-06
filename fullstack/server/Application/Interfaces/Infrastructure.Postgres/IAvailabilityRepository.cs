using Application.Services;
using Core.Domain.Entities;

namespace Application.Interfaces.Infrastructure.Postgres;

public interface IAvailabilityRepository
{
    void AddAvailability(ServiceAvailability availability);
    
    ServiceAvailability GetAvailability(Booking booking, int bookingDay);
    
    void UpdateAvailability(ServiceAvailability availability);
    
    void DeleteAvailability(string id);     
}