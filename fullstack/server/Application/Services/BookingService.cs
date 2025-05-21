using Application.Interfaces;
using Application.Interfaces.Infrastructure.Postgres;
using Application.Interfaces.Infrastructure.Websocket;
using Application.Models.Dtos.Availability;
using Application.Models.Dtos.Booking;
using Core.Domain.Entities;

namespace Application.Services;

public class BookingService(IBookingDataRepository bookingRepository, IAvailabilityRepository availabilityRepository, IConnectionManager connectionManager) : IBookingService
{
    public async Task<BookingResponseDto> CreateBooking(BookingCreateRequestDto dto)
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

        var latestBookings = bookingRepository.GetLatestBookings();
        var bookingsToBroadcast = latestBookings.Select(b => new BookingResponseDto()
        {
            Id = b.Id,
            UserId = b.UserId,
            ServiceName = b.Service.Name,
            Email = b.User.Email,
            Date = b.Date.ToString("dd-MM-yyyy"),
            StartTime = b.StartTime.ToString("HH:mm"),
            EndTime = b.EndTime.ToString("HH:mm"),
            CreatedAt = b.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"),
            UpdatedAt = b.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"),
        }).ToList();
        
        var bookingsBroadcastDto = new BookingsBroadcastDto
        {
            eventType = "BookingsBroadcastDto",
            bookings = bookingsToBroadcast
        };
        
        await connectionManager.BroadcastToTopic("dashboard", bookingsBroadcastDto);
        
        return new BookingResponseDto()
        {
            Id = createdBooking.Id,
            UserId = createdBooking.UserId,
            ServiceName = createdBooking.Service.Name,
            Email = createdBooking.User.Email,
            Date = createdBooking.Date.ToString("dd-MM-yyyy"),
            StartTime = createdBooking.StartTime.ToString("HH:mm"),
            EndTime = createdBooking.EndTime.ToString("HH:mm"),
            CreatedAt = createdBooking.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"),
            UpdatedAt = createdBooking.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"),
        };
    }

    public List<BookingResponseDto> GetAllBookings()
    {
        var bookings = bookingRepository.GetAllBookings();

        return bookings.Select(b => new BookingResponseDto()
        {
            Id = b.Id,
            UserId = b.UserId,
            ServiceName = b.Service.Name,
            Email = b.User.Email,
            Date = b.Date.ToString("dd-MM-yyyy"),
            StartTime = b.StartTime.ToString("HH:mm"),
            EndTime = b.EndTime.ToString("HH:mm"),
            CreatedAt = b.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"),
            UpdatedAt = b.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"),
        }).ToList();
    }

    public List<BookingResponseDto> GetLatestBookings()
    {
        var latestBookings = bookingRepository.GetLatestBookings();

        return latestBookings.Select(b => new BookingResponseDto()
        {
            Id = b.Id,
            UserId = b.UserId,
            ServiceName = b.Service.Name,
            Email = b.User.Email,
            Date = b.Date.ToString("dd-MM-yyyy"),
            StartTime = b.StartTime.ToString("HH:mm"),
            EndTime = b.EndTime.ToString("HH:mm"),
            CreatedAt = b.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"),
            UpdatedAt = b.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"),
        }).ToList();
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
            ServiceName = deletedBooking.Service.Name,
            Email = deletedBooking.User.Email,
            Date = deletedBooking.Date.ToString("dd-MM-yyyy"),
            StartTime = deletedBooking.StartTime.ToString("HH:mm"),
            EndTime = deletedBooking.EndTime.ToString("HH:mm"),
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