using Application.Interfaces;
using Application.Interfaces.Infrastructure.Postgres;
using Application.Models.Dtos.Availability;
using Application.Models.Dtos.Booking;
using Core.Domain.Entities;

namespace Application.Services;

public class BookingService(IBookingDataRepository bookingRepository, IAvailabilityRepository availabilityRepository) : IBookingService
{
    public BookingResponseDto CreateBooking(BookingCreateRequestDto dto)
    {
        var booking = new Booking
        {
            Id = Guid.NewGuid().ToString(),
            UserId = dto.UserId,
            ServiceId = dto.ServiceId,
            Date = dto.Date,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now

        };
        if (!CanCreateBooking(booking)) throw new InvalidOperationException("Booking could not be created.");
        
        var createdBooking = bookingRepository.CreateBooking(booking);
        return new BookingResponseDto()
        {
            Id = createdBooking.Id,
            UserId = createdBooking.UserId,
            ServiceId = createdBooking.ServiceId,
            Date = createdBooking.Date.ToString("dd-MM-yyyy"),
            StartTime = createdBooking.StartTime.ToString(),
            EndTime = createdBooking.EndTime.ToString(),
            CreatedAt = createdBooking.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"),
            UpdatedAt = createdBooking.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"),
        };
    }

    private bool CanCreateBooking(Booking newBooking)
    {
        var bookingDay = (int)newBooking.Date.DayOfWeek;

        var availability =  availabilityRepository.GetAvailability(newBooking, bookingDay);

        if (availability == null)
            throw new InvalidOperationException("No availability for this service on the selected day.");

        var startTime = newBooking.StartTime;
        var endTime = newBooking.EndTime;

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
        var deletedBooking = bookingRepository.DeleteBooking(id);
        return new BookingResponseDto()
        {
            Id = deletedBooking.Id,
            UserId = deletedBooking.UserId,
            ServiceId = deletedBooking.ServiceId,
            Date = deletedBooking.Date.ToString("dd-MM-yyyy"),
            StartTime = deletedBooking.StartTime.ToString(),
            EndTime = deletedBooking.EndTime.ToString(),
            CreatedAt = deletedBooking.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"),
            UpdatedAt = deletedBooking.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"),
        };
    }

    public List<AvailabiltySlotsDto> GetAvailabilitySlots(string serviceId)
    {
        var today = DateTime.Today;
        var availabilitySlots = new List<AvailabiltySlotsDto>();

        for (int i = 0; i < 7; i++)
        {
            var date = today.AddDays(i);
            int dayOfWeek = (int)date.DayOfWeek;
            var availability = availabilityRepository.GetAvailabilityForServiceAndDay(serviceId, dayOfWeek);

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
                    StartTime = time,
                    EndTime = time.AddHours(1)
                });
            }
        }

        return availabilitySlots;
        
    }
}