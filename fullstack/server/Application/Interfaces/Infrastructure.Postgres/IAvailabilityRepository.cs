using Application.Services;
using Core.Domain.Entities;

namespace Application.Interfaces.Infrastructure.Postgres;

public interface IAvailabilityRepository
{
    Task<ServiceAvailability> CreateAvailability(ServiceAvailability availability);
    
    Task<List<ServiceAvailability>> CreateAllAvailabilities(List<ServiceAvailability> availabilities);
    
    Task<ServiceAvailability> GetAvailability(Booking booking, int bookingDay);
    
    Task<ServiceAvailability> UpdateAvailability(ServiceAvailability availability);
    
    Task<ServiceAvailability> DeleteAvailability(string id);     
    
    Task<ServiceAvailability> GetAvailabilityForServiceAndDay(string serviceId, int dayOfWeek);
}