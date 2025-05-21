using Application.Interfaces;
using Application.Interfaces.Infrastructure.Postgres;
using Application.Models.Dtos.Availability;
using Core.Domain.Entities;

namespace Application.Services;

public class AvailabilityService(IAvailabilityRepository repository) : IAvailabilityService
{
    public AvailabilityResponseDto CreateAvailability(AvailabilityCreateRequestDto dto)
    {
        var createdAvailability = repository.CreateAvailability(new ServiceAvailability()
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
            Id = createdAvailability.Id,
            ServiceId = createdAvailability.ServiceId,
            DayOfWeek = createdAvailability.DayOfWeek,
            AvailableFrom = createdAvailability.AvailableFrom,
            AvailableTo = createdAvailability.AvailableTo,
            CreatedAt = createdAvailability.CreatedAt,
            UpdatedAt = createdAvailability.UpdatedAt
        };
    }

    public List<AvailabilityResponseDto> CreateAllAvailabilities(List<AvailabilityCreateRequestDto> dtos)
    {
        var availabilities = dtos.Select(dto => new ServiceAvailability()
            {
                Id = Guid.NewGuid().ToString(),
                ServiceId = dto.ServiceId,
                DayOfWeek = dto.DayOfWeek,
                AvailableFrom = dto.AvailableFrom,
                AvailableTo = dto.AvailableTo,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            }
        ).ToList();

        var createdAvailabilities = repository.CreateAllAvailabilities(availabilities);
        return createdAvailabilities.Select(createdAvailability => new AvailabilityResponseDto()
            {
                Id = createdAvailability.Id,
                ServiceId = createdAvailability.ServiceId,
                DayOfWeek = createdAvailability.DayOfWeek,
                AvailableFrom = createdAvailability.AvailableFrom,
                AvailableTo = createdAvailability.AvailableTo,
                CreatedAt = createdAvailability.CreatedAt,
                UpdatedAt = createdAvailability.UpdatedAt
            }
        ).ToList();
    }

    public AvailabilityResponseDto DeleteAvailability(string id)
    {
        var deletedAvailability = repository.DeleteAvailability(id);
        return new AvailabilityResponseDto()
        {
            Id = deletedAvailability.Id,
            ServiceId = deletedAvailability.ServiceId,
            DayOfWeek = deletedAvailability.DayOfWeek,
            AvailableFrom = deletedAvailability.AvailableFrom,
            AvailableTo = deletedAvailability.AvailableTo,
            CreatedAt = deletedAvailability.CreatedAt,
            UpdatedAt = deletedAvailability.UpdatedAt
        };
    }

    public AvailabilityResponseDto UpdateAvailability(AvailabilityUpdateRequestDto dto)
    {
        var availability = new ServiceAvailability()
        {
            Id = dto.Id,
            ServiceId = dto.ServiceId,
            DayOfWeek = dto.DayOfWeek,
            AvailableFrom = dto.AvailableFrom,
            AvailableTo = dto.AvailableTo,
        };
        var updateAvailability = repository.UpdateAvailability(availability);
        return new AvailabilityResponseDto()
        {
            Id = updateAvailability.Id,
            ServiceId = updateAvailability.ServiceId,
            DayOfWeek = updateAvailability.DayOfWeek,
            AvailableFrom = updateAvailability.AvailableFrom,
            AvailableTo = updateAvailability.AvailableTo,
            CreatedAt = updateAvailability.CreatedAt,
            UpdatedAt = updateAvailability.UpdatedAt
        };
    }
}