using Fleck;
using WebSocketBoilerplate;

namespace Api.Websocket.EventHandlers;

public class AdminWantsToJoinDashboardDto : BaseDto
{
    public string ClientId { get; set; } = null!;
}

public class ServerResponseToJoinDashboard : BaseDto
{
    public string Message { get; set; } = null!;
}

public class AdminWantsToJoinDashboardHandler : BaseEventHandler<AdminWantsToJoinDashboardDto>
{
    public override Task Handle(AdminWantsToJoinDashboardDto dto, IWebSocketConnection socket)
    {
        var clientId = dto.ClientId;
        var message = $"Admin with ID {clientId} wants to join the dashboard.";
        socket.SendDto(new ServerResponseToJoinDashboard
        {
            Message = message
        });
        
        
        return Task.CompletedTask;
    }
}