using Amazon.SimpleNotificationService;
using Amazon.SQS;

namespace Infra.MessageBus.Client
{
    public interface IMessageBusClient
    {
        AmazonSQSClient QueueClient { get; }
        AmazonSimpleNotificationServiceClient TopicClient { get; }
    }
}
