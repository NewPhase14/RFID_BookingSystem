namespace Application.Interfaces;

public interface ISubscriptionService
{
    public Task SubscribeToDashboard(string clientId);
}