using Application.Interfaces;
using Application.Interfaces.Infrastructure.Postgres;
using Application.Models.Dtos.Availability;
using Core.Domain.Entities;

namespace Application.Services;

public class AvailabilityService(IAvailabilityRepository availabilityRepository, IBookingRepository bookingRepository)
    : IAvailabilityService
{
    public async Task<AvailabilityResponseDto> CreateAvailability(AvailabilityCreateRequestDto dto)
    {
        var europeanTime = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
        var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, europeanTime);
        
        var createdAvailability = await availabilityRepository.CreateAvailability(new ServiceAvailability
            {
                Id = Guid.NewGuid().ToString(),
                ServiceId = dto.ServiceId,
                DayOfWeek = dto.DayOfWeek,
                AvailableFrom = dto.AvailableFrom,
                AvailableTo = dto.AvailableTo,
                CreatedAt = now,
                UpdatedAt = now
            }
        );
        return new AvailabilityResponseDto
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

    public async Task<List<AvailabilityResponseDto>> CreateAllAvailabilities(List<AvailabilityCreateRequestDto> dtos)
    {
        var europeanTime = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
        var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, europeanTime);
        
        var availabilities = dtos.Select(dto => new ServiceAvailability
            {
                Id = Guid.NewGuid().ToString(),
                ServiceId = dto.ServiceId,
                DayOfWeek = dto.DayOfWeek,
                AvailableFrom = dto.AvailableFrom,
                AvailableTo = dto.AvailableTo,
                CreatedAt = now,
                UpdatedAt = now
            }
        ).ToList();

        var createdAvailabilities = await availabilityRepository.CreateAllAvailabilities(availabilities);
        return createdAvailabilities.Select(createdAvailability => new AvailabilityResponseDto
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

    public async Task<AvailabilityResponseDto> DeleteAvailability(string id)
    {
        var deletedAvailability = await availabilityRepository.DeleteAvailability(id);
        return new AvailabilityResponseDto
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

    public async Task<AvailabilityResponseDto> UpdateAvailability(AvailabilityUpdateRequestDto dto)
    {
        var availability = new ServiceAvailability
        {
            Id = dto.Id,
            ServiceId = dto.ServiceId,
            DayOfWeek = dto.DayOfWeek,
            AvailableFrom = dto.AvailableFrom,
            AvailableTo = dto.AvailableTo
        };
        var updateAvailability = await availabilityRepository.UpdateAvailability(availability);
        return new AvailabilityResponseDto
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
        var europeanTime = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
        var now = TimeZoneInfo.ConvertTime(DateTime.UtcNow, europeanTime);
        var today = DateOnly.FromDateTime(now);

        var availabilitySlots = new List<AvailabiltySlotsDto>();

        for (var i = 0; i < 7; i++)
        {
            var date = today.AddDays(i);
            Console.WriteLine(date);
            var dayOfWeek = (int)date.DayOfWeek;
            var availability = await availabilityRepository.GetAvailabilityForServiceAndDay(serviceId, dayOfWeek);

            if (availability == null)
                continue;

            var slotStart = availability.AvailableFrom;
            var slotEnd = availability.AvailableTo;

            var bookings = await bookingRepository.GetBookingsForServiceAndDate(serviceId, date);

            for (var time = slotStart; time < slotEnd; time = time.AddHours(1))
            {
                var slotStartTime = time;
                var slotEndTime = time.AddHours(1);
                var slotDateTime = date.ToDateTime(time);

                if (i == 0 && slotDateTime < now)
                    continue;

                var isOverlapping = bookings.Any(b =>
                    slotStartTime < b.EndTime && b.StartTime < slotEndTime
                );

                if (isOverlapping)
                    continue;

                availabilitySlots.Add(new AvailabiltySlotsDto
                {
                    Date = date.ToString("dd-MM-yyyy"),
                    StartTime = slotStartTime.ToString("HH:mm"),
                    EndTime = slotEndTime.ToString("HH:mm")
                });
            }
        }

        return availabilitySlots;
    }
}