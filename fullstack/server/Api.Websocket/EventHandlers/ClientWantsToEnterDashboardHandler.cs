using System.Security.Authentication;
using Application.Interfaces;
using Application.Interfaces.Infrastructure.Websocket;
using Fleck;
using WebSocketBoilerplate;

namespace Api.Websocket.EventHandlers;

public class ClientWantsToEnterDashboardDto : BaseDto
{
    public string Jwt { get; set; }   
}

public class ClientWantsToEnterDashboard(
    IConnectionManager connectionManager, 
    ISecurityService securityService)
    : BaseEventHandler<ClientWantsToEnterDashboardDto>
{
    public override async Task Handle(ClientWantsToEnterDashboardDto dto, IWebSocketConnection socket)
    {
        var claims = securityService.VerifyJwtOrThrow(dto.Jwt);
        
        if (claims.Role != "Admin")
        {
            throw new AuthenticationException("Unauthorized: Only admins can access the dashboard");
        }
        
        var clientId =  connectionManager.GetClientIdFromSocket(socket);
        await connectionManager.AddToTopic("dashboard", clientId);
        socket.SendDto(new ServerConfirmsAdditionToDashboard() {requestId = dto.requestId});
    }
}

public class ServerConfirmsAdditionToDashboard : BaseDto
{
    public string Message { get; set; } = "You have been added to the dashboard";
}
