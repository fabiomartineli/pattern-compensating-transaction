namespace Infra.MessageBus.Publisher
{
    public record MessageBusTopicPublisherRequest<TContent>
    {
        public required string Destination { get; init; }
        public required string Id { get; init; }
        public required TContent Content { get; init; }
    }
}
