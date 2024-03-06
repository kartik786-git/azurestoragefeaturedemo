using azurestoragefeaturedemo.api.ViewModel;

namespace azurestoragefeaturedemo.api.Services
{
    public interface IAzureServiceBusService
    {
        Task SendMessageAsync(string message);
        Task SendMessageAsync<T>(T message);
        Task<List<MessageResponse>> RecivedMessageAsync(int msgCount);
        Task<List<MessageResponse>> RecivedMessageDeleteAsync(int msgCount = 5);
    }
}
