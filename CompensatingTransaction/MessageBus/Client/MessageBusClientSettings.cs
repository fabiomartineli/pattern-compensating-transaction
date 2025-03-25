namespace Infra.MessageBus.Client
{
    public record MessageBusClientSettings
    {
        public required string BaseQueueUrl { get; init; }
        public required string BaseTopicUrl { get; init; }
        public required string AccessKey { get; init; }
        public required string SecretKey { get; init; }
    }
}
