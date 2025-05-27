using Application.Interfaces;
using Application.Interfaces.Infrastructure.Postgres;
using Application.Interfaces.Infrastructure.Websocket;
using Application.Models.Dtos.Booking;
using Core.Domain.Entities;

namespace Application.Services;

public class BookingService(
    IBookingRepository bookingRepository,
    IAvailabilityRepository availabilityRepository,
    IConnectionManager connectionManager) : IBookingService
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
        if (!await CanCreateBooking(booking)) throw new InvalidOperationException("Booking could not be created.");

        var createdBooking = await bookingRepository.CreateBooking(booking);

        var latestBookings = await bookingRepository.GetLatestBookings();
        var bookingsToBroadcast = latestBookings.Select(b => new BookingResponseDto
        {
            Id = b.Id,
            UserId = b.UserId,
            ServiceName = b.Service.Name,
            Email = b.User.Email,
            Date = b.Date.ToString("dd-MM-yyyy"),
            StartTime = b.StartTime.ToString("HH:mm"),
            EndTime = b.EndTime.ToString("HH:mm"),
            CreatedAt = b.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"),
            UpdatedAt = b.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss")
        }).ToList();

        var bookingsBroadcastDto = new BookingsBroadcastDto
        {
            eventType = "BookingsBroadcastDto",
            bookings = bookingsToBroadcast
        };

        await connectionManager.BroadcastToTopic("dashboard", bookingsBroadcastDto);
        return new BookingResponseDto
        {
            Id = createdBooking.Id,
            UserId = createdBooking.UserId,
            Email = createdBooking.User.Email,
            ServiceName = createdBooking.Service.Name,
            Date = createdBooking.Date.ToString("dd-MM-yyyy"),
            StartTime = createdBooking.StartTime.ToString("HH:mm"),
            EndTime = createdBooking.EndTime.ToString("HH:mm"),
            CreatedAt = createdBooking.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"),
            UpdatedAt = createdBooking.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss")
        };
    }

    public async Task<List<BookingResponseDto>> GetAllBookings()
    {
        var bookings = await bookingRepository.GetAllBookings();
        return bookings.Select(b => new BookingResponseDto
        {
            Id = b.Id,
            UserId = b.UserId,
            ServiceName = b.Service.Name,
            Email = b.User.Email,
            Date = b.Date.ToString("dd-MM-yyyy"),
            StartTime = b.StartTime.ToString("HH:mm"),
            EndTime = b.EndTime.ToString("HH:mm"),
            CreatedAt = b.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"),
            UpdatedAt = b.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss")
        }).ToList();
    }

    public async Task<List<BookingResponseDto>> GetLatestBookings()
    {
        var latestBookings = await bookingRepository.GetLatestBookings();

        return latestBookings.Select(b => new BookingResponseDto
        {
            Id = b.Id,
            UserId = b.UserId,
            ServiceName = b.Service.Name,
            Email = b.User.Email,
            Date = b.Date.ToString("dd-MM-yyyy"),
            StartTime = b.StartTime.ToString("HH:mm"),
            EndTime = b.EndTime.ToString("HH:mm"),
            CreatedAt = b.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"),
            UpdatedAt = b.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss")
        }).ToList();
    }

    public async Task<BookingResponseDto> DeleteBooking(string id)
    {
        var deletedBooking = await bookingRepository.DeleteBooking(id);
        return new BookingResponseDto
        {
            Id = deletedBooking.Id,
            UserId = deletedBooking.UserId,
            ServiceName = deletedBooking.Service.Name,
            Email = deletedBooking.User.Email,
            Date = deletedBooking.Date.ToString("dd-MM-yyyy"),
            StartTime = deletedBooking.StartTime.ToString("HH:mm"),
            EndTime = deletedBooking.EndTime.ToString("HH:mm"),
            CreatedAt = deletedBooking.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"),
            UpdatedAt = deletedBooking.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss")
        };
    }

    public async Task<List<BookingResponseDto>> GetTodaysBookingsByUserId(string userId)
    {
        var bookings = await bookingRepository.GetTodaysBookingsByUserId(userId);

        return bookings.Select(b => new BookingResponseDto
        {
            Id = b.Id,
            UserId = b.UserId,
            ServiceName = b.Service.Name,
            Email = b.User.Email,
            Date = b.Date.ToString("dd-MM-yyyy"),
            StartTime = b.StartTime.ToString("HH:mm"),
            EndTime = b.EndTime.ToString("HH:mm"),
            CreatedAt = b.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"),
            UpdatedAt = b.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss")
        }).ToList();
    }

    public async Task<List<BookingResponseDto>> GetFutureBookingsByUserId(string userId)
    {
        var bookings = await bookingRepository.GetFutureBookingsByUserId(userId);

        return bookings.Select(b => new BookingResponseDto
        {
            Id = b.Id,
            UserId = b.UserId,
            ServiceName = b.Service.Name,
            Email = b.User.Email,
            Date = b.Date.ToString("dd-MM-yyyy"),
            StartTime = b.StartTime.ToString("HH:mm"),
            EndTime = b.EndTime.ToString("HH:mm"),
            CreatedAt = b.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"),
            UpdatedAt = b.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss")
        }).ToList();
    }

    public async Task<List<BookingResponseDto>> GetPastBookingsByUserId(string userId)
    {
        var bookings = await bookingRepository.GetPastBookingsByUserId(userId);

        return bookings.Select(b => new BookingResponseDto
        {
            Id = b.Id,
            UserId = b.UserId,
            ServiceName = b.Service.Name,
            Email = b.User.Email,
            Date = b.Date.ToString("dd-MM-yyyy"),
            StartTime = b.StartTime.ToString("HH:mm"),
            EndTime = b.EndTime.ToString("HH:mm"),
            CreatedAt = b.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"),
            UpdatedAt = b.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss")
        }).ToList();
    }

    private async Task<bool> CanCreateBooking(Booking newBooking)
    {
        var bookingDay = (int)newBooking.Date.DayOfWeek;

        var availability = await availabilityRepository.GetAvailability(newBooking, bookingDay);
        if (availability == null)
            throw new InvalidOperationException("No availability for this service on this selected day");

        var startTime = newBooking.StartTime;
        var endTime = newBooking.EndTime;

        if (startTime < availability.AvailableFrom || endTime > availability.AvailableTo)
            throw new InvalidOperationException("Booking time is outside the available service hours.");

        // Overlapping check
        var overlaps = await bookingRepository.BookingOverlapping(newBooking);

        if (overlaps)
            throw new InvalidOperationException("This booking overlaps with an existing booking.");

        return true;
    }
}