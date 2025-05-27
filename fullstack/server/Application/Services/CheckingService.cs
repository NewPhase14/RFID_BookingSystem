using Application.Interfaces;
using Application.Interfaces.Infrastructure.Postgres;
using Application.Interfaces.Infrastructure.Websocket;
using Application.Models.Dtos.ActivityLog;
using Application.Models.Dtos.Checking;
using Core.Domain.Entities;

namespace Application.Services;

public class CheckingService(
    ICheckingRepository checkingRepository,
    IActivityLogsRepository activityLogsRepository,
    IConnectionManager connectionManager) : ICheckingService
{
    public async Task<CheckingResponseDto> CheckIfValid(CheckBookingRequestDto dto)
    {
        var europeanTime = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
        var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, europeanTime);
        
        var activity = await checkingRepository.CheckBookingRequestDto(dto.Rfid, dto.ServiceId);

        var activityLog = new ActivityLog
        {
            Id = Guid.NewGuid().ToString(),
            ServiceId = dto.ServiceId,
            AttemptedAt = now,
            UserId = activity.UserId,
            Status = activity.Status
        };

        await activityLogsRepository.CreateActivityLogs(activityLog);

        var latestActivityLogs = await activityLogsRepository.GetLatestActivityLogs();

        var activityLogsToBroadcast = latestActivityLogs.Select(log => new ActivityLogResponseDto
        {
            Id = log.Id,
            ServiceName = log.Service.Name,
            Date = log.AttemptedAt.ToString("dd-MM-yyyy"),
            Time = log.AttemptedAt.ToString("HH:mm:ss"),
            Fullname = log.User.FirstName + " " + log.User.LastName,
            Status = log.Status
        }).ToList();

        var activityLogsBroadcastDto = new ActivityLogsBroadcastDto
        {
            eventType = "ActivityLogsBroadcastDto",
            activityLogs = activityLogsToBroadcast
        };

        await connectionManager.BroadcastToTopic("dashboard", activityLogsBroadcastDto);

        return new CheckingResponseDto
        {
            Message = activity.IsValid ? "Valid" : "Invalid"
        };
    }
}