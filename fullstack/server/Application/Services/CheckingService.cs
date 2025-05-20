using Application.Interfaces;
using Application.Interfaces.Infrastructure.Postgres;
using Application.Interfaces.Infrastructure.Websocket;
using Application.Models.Dtos.ActivityLog;
using Application.Models.Dtos.Checking;
using Core.Domain.Entities;

namespace Application.Services;

public class CheckingService(ICheckingRepository checkingRepository, IActivityLogsRepository activityLogsRepository, IConnectionManager connectionManager) : ICheckingService
{
    public async Task<CheckingResponseDto> CheckIfValid(CheckBookingRequestDto dto)
    {
        var activity = checkingRepository.CheckBookingRequestDto(dto.Rfid, dto.ServiceId);

        var activityLog = new ActivityLog
        {
            Id = Guid.NewGuid().ToString(),
            ServiceId = dto.ServiceId,
            AttemptedAt = DateTime.Now,
            UserId = activity.UserId,
            Status = activity.Status,
        };

        activityLogsRepository.AddActivityLogs(activityLog);

        var logs = activityLogsRepository.GetLatestActivityLogs();

        var logsDto = logs.Select(log => new ActivityLogDto
        {
            Id = log.Id,
            ServiceName = log.Service.Name,
            Date = log.AttemptedAt.ToString("dd-MM-yyyy"),
            Time = log.AttemptedAt.ToString("HH:mm:ss"),
            Fullname = log.User.FirstName + " " + log.User.LastName,
            Status = log.Status,
        }).ToList();

        var logsResponse = new ActivityLogsResponseDto
        {
            eventType = "ActivityLogsResponseDto",
            activityLogs = logsDto
        };

        await connectionManager.BroadcastToTopic("dashboard", logsResponse); 
            
        return new CheckingResponseDto
        {
            Message = activity.IsValid ? "Valid" : "Invalid"
        };
    }
}