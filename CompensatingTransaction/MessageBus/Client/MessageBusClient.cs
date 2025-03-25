using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SQS;
using Microsoft.Extensions.Options;

namespace Infra.MessageBus.Client
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly MessageBusClientSettings _settings;
        private AmazonSQSClient _client;
        private AmazonSimpleNotificationServiceClient _topicClient;

        public MessageBusClient(IOptions<MessageBusClientSettings> settings)
        {
            _settings = settings.Value;
        }

        public AmazonSQSClient QueueClient 
        { 
            get 
            {
                _client ??= new AmazonSQSClient(_settings.AccessKey, _settings.SecretKey, RegionEndpoint.USEast2);

                return _client;
            } 
        }

        public AmazonSimpleNotificationServiceClient TopicClient
        {
            get
            {
                _topicClient ??= new AmazonSimpleNotificationServiceClient(_settings.AccessKey, _settings.SecretKey, RegionEndpoint.USEast2);

                return _topicClient;
            }
        }
    }
}
