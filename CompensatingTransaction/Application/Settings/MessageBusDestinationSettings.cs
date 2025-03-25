namespace Application.Settings
{
    public record MessageBusDestinationSettings
    {
        // QUEUE
        public string QueueOrderCreate { get; init; }
        public string QueueOrderCancel { get; init; }
        public string QueueOrderConfirmPayment { get; init; }
        public string QueueOrderConfirmStock { get; init; }
        public string QueueOrderConfirmDelivery { get; init; }
        public string QueueOrderStartPayment { get; init; }

        public string QueuePaymentRequest { get; init; }
        public string QueuePaymentConfirm { get; init; }
        public string QueuePaymentCancel { get; init; }
        public string QueuePaymentFail { get; init; }

        public string QueueStockCreate { get; init; }
        public string QueueStockCancel { get; init; }

        public string QueueDeliveryCreate { get; init; }
        public string QueueDeliveryConfirm { get; init; }
        public string QueueDeliveryFail { get; init; }

        // TOPIC
        public string TopicOrderCreated { get; init; }

        public string TopicPaymentRequested { get; init; }
        public string TopicPaymentConfirmed { get; init; }
        public string TopicPaymentFailed { get; init; }

        public string TopicStockConfirmed { get; init; }
        public string TopicStockFailed { get; init; }

        public string TopicDeliveryFailed { get; init; }
        public string TopicDeliveryConfirmed { get; init; }
    }
}
