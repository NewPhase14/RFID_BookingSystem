using Application.Interfaces;
using Application.Interfaces.Infrastructure.Postgres;
using Application.Models;
using Application.Models.Dtos.TimeSlot;
using Core.Domain.Entities;
using Microsoft.Extensions.Options;

namespace Application.Services;

public class TimeSlotService(IOptionsMonitor<AppOptions> optionsMonitor, ITimeSlotRepository repository) : ITimeSlotService
{
    public TimeSlotResponseDto CreateTimeSlot(TimeSlotCreateRequestDto dto)
    {
        repository.AddTimeSlot(new ServiceTimeSlot
            {
            Id = Guid.NewGuid().ToString(),
            ServiceId = dto.ServiceId,
            DayOfWeek = dto.WeekdayId,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            CreatedAt = DateTime.Now 
            }
        );
        return new TimeSlotResponseDto()
        {
            Message = "Time slot created successfully",
        };
    }
}