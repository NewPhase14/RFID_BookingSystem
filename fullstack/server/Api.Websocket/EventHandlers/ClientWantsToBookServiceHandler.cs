using Fleck;
using WebSocketBoilerplate;

namespace Api.Websocket.EventHandlers;

public class ClientWantsToBookServiceDto : BaseDto
{
    public string ClientId { get; set; } = null!;
    public string ServiceId { get; set; } = null!;
}

public class ServerResponseToBookService : BaseDto
{
    public string Message { get; set; } = null!;
    public string ServiceId { get; set; } = null!;
    public string ClientId { get; set; } = null!;
}

public class ClientWantsToBookServiceHandler : BaseEventHandler<ClientWantsToBookServiceDto>
{
    public override Task Handle(ClientWantsToBookServiceDto dto, IWebSocketConnection socket)
    {
        throw new NotImplementedException();
    }
}