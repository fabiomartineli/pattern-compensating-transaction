using Infra.MessageBus.Client;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Infra.MessageBus.Publisher
{
    public class MessageBusPublisher : IMessageBusPublisher
    {
        private readonly IMessageBusClient _messageBusClient;
        private readonly MessageBusClientSettings _settings;

        public MessageBusPublisher(IMessageBusClient messageBusClient, IOptions<MessageBusClientSettings> settings)
        {
            _messageBusClient = messageBusClient;
            _settings = settings.Value;
        }

        public async Task ExecuteAsync<TContent>(MessageBusQueuePublisherRequest<TContent> request, CancellationToken cancellationToken)
        {
            var url = $"{_settings.BaseQueueUrl}{request.Destination}";
            await _messageBusClient.QueueClient.SendMessageAsync(url, JsonSerializer.Serialize(request.Content), cancellationToken);
        }

        public async Task ExecuteAsync<TContent>(MessageBusTopicPublisherRequest<TContent> request, CancellationToken cancellationToken)
        {
            var url = $"{_settings.BaseTopicUrl}{request.Destination}";
            await _messageBusClient.TopicClient.PublishAsync(url, JsonSerializer.Serialize(request.Content), cancellationToken);
        }
    }
}
