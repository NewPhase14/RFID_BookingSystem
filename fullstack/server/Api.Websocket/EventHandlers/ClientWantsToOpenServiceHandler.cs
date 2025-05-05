using Fleck;
using WebSocketBoilerplate;

namespace Api.Websocket.EventHandlers;

public class ClientWantsToOpenServiceDto : BaseDto
{
    public string ServiceId { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public string Rfid { get; set; } = null!;
}

public class ServerResponseToOpenService : BaseDto
{
    public string Message { get; set; } = null!;
    public string ServiceId { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public string Rfid { get; set; } = null!;
}

public class ClientWantsToOpenServiceHandler : BaseEventHandler<ClientWantsToOpenServiceDto>
{
    public override Task Handle(ClientWantsToOpenServiceDto dto, IWebSocketConnection socket)
    {
        throw new NotImplementedException();
    }
}