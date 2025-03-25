namespace Infra.MessageBus.Consumer
{
    public readonly struct MessageBusConsumerRequest
    {
        public required string Destination { get; init; }
        public string CompensatingTransactionDestination { get; init; }
    }
}
