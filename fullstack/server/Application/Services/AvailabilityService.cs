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
    
    public async Task<List<AvailabiltySlotsDto>> GetAvailabilitySlots(string serviceId)
    {
        var today = DateTime.Today;
        var availabilitySlots = new List<AvailabiltySlotsDto>();

        for (int i = 0; i < 7; i++)
        {
            var date = today.AddDays(i);
            int dayOfWeek = (int)date.DayOfWeek;
            var availability = await repository.GetAvailabilityForServiceAndDay(serviceId, dayOfWeek);

            if (availability == null)
                continue;

            var slotStart = availability.AvailableFrom;
            var slotEnd = availability.AvailableTo;

            for (var time = slotStart; time < slotEnd; time = time.AddHours(1))
            {
                var slotDateTime = date.Add(time.ToTimeSpan());
                
                if (i == 0 && slotDateTime < DateTime.Now)
                    continue;

                availabilitySlots.Add(new AvailabiltySlotsDto
                {
                    Date = date.ToString("dd-MM-yyyy"),
                    StartTime = time.ToString("HH:mm"),
                    EndTime = time.AddHours(1).ToString("HH:mm")
                });
            }
        }

        return availabilitySlots;
    }
}