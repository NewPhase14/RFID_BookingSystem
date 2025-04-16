using Application.Interfaces;
using Application.Interfaces.Infrastructure.Postgres;
using Application.Models;
using Application.Models.Dtos.Booking;
using Core.Domain.Entities;
using Microsoft.Extensions.Options;

namespace Application.Services;

public class BookingService(IOptionsMonitor<AppOptions> optionsMonitor, IBookingDataRepository bookingRepository, IAvailabilityRepository availabilityRepository) : IBookingService
{
    public BookingResponseDto CreateBooking(BookingCreateRequestDto dto)
    {
        var booking = new Booking
        {
            Id = Guid.NewGuid().ToString(),
            UserId = dto.UserId,
            ServiceId = dto.ServiceId,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now

        };
        if (CanCreateBooking(booking))
        {
            bookingRepository.AddBooking(booking);
        }

        return new BookingResponseDto()
        {
            Message = "Booking created successfully",
        };
    }
    
    public bool CanCreateBooking(Booking newBooking)
    {
        var bookingDay = (int)newBooking.StartTime.DayOfWeek;

        var availability =  availabilityRepository.GetAvailability(newBooking, bookingDay);

        if (availability == null)
            throw new InvalidOperationException("No availability for this service on the selected day.");

        var startTime = TimeOnly.FromTimeSpan(newBooking.StartTime.TimeOfDay);
        var endTime = TimeOnly.FromTimeSpan(newBooking.EndTime.TimeOfDay);

        if (startTime < availability.AvailableFrom || endTime > availability.AvailableTo)
            throw new InvalidOperationException("Booking time is outside the available service hours.");

        // Overlapping check
        var overlaps = bookingRepository.BookingOverlapping(newBooking);

        if (overlaps)
            throw new InvalidOperationException("This booking overlaps with an existing booking.");

        return true;
    }

    public BookingResponseDto DeleteBooking(string id)
    {
        bookingRepository.DeleteBooking(id);
        return new BookingResponseDto()
        {
            Message = "Booking deleted successfully",
        };
    }
}