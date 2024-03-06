using Azure.Core.Extensions;
using Azure.Storage.Queues;
using Microsoft.Extensions.Azure;

namespace azurestoragefeaturedemo.api
{
    internal static class AzureClientFactoryBuilderExtensions
    {
        public static IAzureClientBuilder<QueueServiceClient, QueueClientOptions>
             AddQueueServiceClient(this AzureClientFactoryBuilder builder,
            string serviceUriOrConnectionString, bool preferMsi)
        {
            if (preferMsi && Uri.TryCreate(serviceUriOrConnectionString, 
                UriKind.Absolute, out Uri? serviceUri))
            {
                return builder.AddQueueServiceClient(serviceUri);
            }
            else
            {
               return builder.AddQueueServiceClient(serviceUriOrConnectionString);
            }
        }
    }
}
