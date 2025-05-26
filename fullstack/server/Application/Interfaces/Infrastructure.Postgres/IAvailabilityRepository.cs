using Application.Services;
using Core.Domain.Entities;

namespace Application.Interfaces.Infrastructure.Postgres;

public interface IAvailabilityRepository
{
    ServiceAvailability CreateAvailability(ServiceAvailability availability);
    
    List<ServiceAvailability> CreateAllAvailabilities(List<ServiceAvailability> availabilities);
    
    Task<ServiceAvailability> GetAvailability(Booking booking, int bookingDay);
    
    ServiceAvailability UpdateAvailability(ServiceAvailability availability);
    
    ServiceAvailability DeleteAvailability(string id);     
    
    Task<ServiceAvailability> GetAvailabilityForServiceAndDay(string serviceId, int dayOfWeek);
}