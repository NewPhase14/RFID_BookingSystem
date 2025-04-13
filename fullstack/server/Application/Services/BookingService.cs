using Application.Interfaces;
using Application.Interfaces.Infrastructure.Postgres;
using Application.Models;
using Application.Models.Dtos.Booking;
using Core.Domain.Entities;
using Microsoft.Extensions.Options;

namespace Application.Services;

public class BookingService(IOptionsMonitor<AppOptions> optionsMonitor, IBookingDataRepository repository) : IBookingService
{
    public BookingResponseDto CreateBooking(BookingCreateRequestDto dto)
    {
        var insertedBooking = repository.AddBooking(new Booking
        {
            Id = Guid.NewGuid().ToString(),
            UserId = dto.UserId,
            ServiceId = dto.ServiceId,
            StatusId = dto.StatusId,
            CreatedAt = DateTime.Now,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
        });
        return new BookingResponseDto()
        {
            Message = "Booking created successfully",
        };
    }
}