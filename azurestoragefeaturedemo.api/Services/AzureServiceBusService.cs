
using Azure.Storage.Queues;
using azurestoragefeaturedemo.api.ViewModel;
using System.Text.Json;
using System.Text.Unicode;

namespace azurestoragefeaturedemo.api.Services
{
    public class AzureServiceBusService : IAzureServiceBusService
    {
        private readonly QueueClient _queueClient;

        public AzureServiceBusService(QueueServiceClient queueServiceClient)
        {
            _queueClient = queueServiceClient.GetQueueClient("testqueue");
        }

        

        public async Task SendMessageAsync(string message)
        {
            await _queueClient.CreateIfNotExistsAsync();
            await _queueClient.SendMessageAsync(message);
        }

        public async Task SendMessageAsync<T>(T message)
        {
            var serializerConfig = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
            var msg = JsonSerializer.Serialize(message, serializerConfig);
            await _queueClient.CreateIfNotExistsAsync();
            await _queueClient.SendMessageAsync(msg);
        }

        public async Task<List<MessageResponse>> RecivedMessageAsync(int msgCount = 5)
        {
            var response = new List<MessageResponse>();
            //var messages = await _queueClient.ReceiveMessagesAsync(msgCount).ConfigureAwait(false);
            var messages = await _queueClient.PeekMessagesAsync(msgCount).ConfigureAwait(false);
            messages.Value.ToList().ForEach( x =>
            {
                var mes = new MessageResponse();
                mes.MessageId = x.MessageId;
                mes.Body = x.MessageText;
                response.Add(mes);
            });
            return response;

        }

        public async Task<List<MessageResponse>> RecivedMessageDeleteAsync(int msgCount = 5)
        {
            var response = new List<MessageResponse>();
            var messages = await _queueClient.ReceiveMessagesAsync(msgCount);

            messages.Value.ToList().ForEach(async x =>
            {
                var mes = new MessageResponse();
                mes.MessageId = x.MessageId;
                mes.Body = x.MessageText;
                response.Add(mes);
                await _queueClient.DeleteMessageAsync(x.MessageId, x.PopReceipt);
            });
            return response;

        }
    }
}
