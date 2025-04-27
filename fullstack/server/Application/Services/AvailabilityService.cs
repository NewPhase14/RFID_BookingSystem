using Application.Interfaces;
using Application.Interfaces.Infrastructure.Postgres;
using Application.Models;
using Application.Models.Dtos.Availability;
using Core.Domain.Entities;
using Microsoft.Extensions.Options;

namespace Application.Services;

public class AvailabilityService(IAvailabilityRepository repository) : IAvailabilityService
{
    public AvailabilityResponseDto CreateAvailability(AvailabilityCreateRequestDto dto)
    {
        repository.AddAvailability(new ServiceAvailability()
            {
            Id = Guid.NewGuid().ToString(),
            ServiceId = dto.ServiceId,
            DayOfWeek = dto.DayOfWeek,
            AvailableFrom = dto.AvailableFrom,
            AvailableTo = dto.AvailableTo,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
            }
        );
        return new AvailabilityResponseDto()
        {
            Message = "Service availability created successfully",
        };
    }
}