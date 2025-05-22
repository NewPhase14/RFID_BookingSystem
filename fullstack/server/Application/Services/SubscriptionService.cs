using Application.Interfaces;
using Application.Interfaces.Infrastructure.Websocket;

namespace Application.Services;

public class SubscriptionService(IConnectionManager connectionManager) : ISubscriptionService
{
    public async Task SubscribeToDashboard(string clientId)
    {
        await connectionManager.AddToTopic("dashboard", clientId);
    }
}