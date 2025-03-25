namespace Infra.MessageBus.Publisher
{
    public record MessageBusQueuePublisherRequest<TContent>
    {
        public required string Destination { get; init; }
        public required string Id { get; init; }
        public required TContent Content { get; init; }
    }
}
