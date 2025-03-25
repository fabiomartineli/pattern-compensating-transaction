using Application.Settings;
using Domain.Commands.Orders.ConfirmPayment;
using Infra.MessageBus.Consumer;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace Api.HostServics.Orders
{
    public class OrderConfirmPaymentConsumer : IHostedService
    {
        private readonly IMessageBusConsumer _consumer;
        private readonly MessageBusDestinationSettings _messageBusDestination;

        public OrderConfirmPaymentConsumer(IMessageBusConsumer consumer,
            IOptions<MessageBusDestinationSettings> options)
        {
            _consumer = consumer;
            _messageBusDestination = options.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _consumer.ExecuteAsync<ConfirmPaymentOrderCommand>(new MessageBusConsumerRequest()
            {
                Destination = _messageBusDestination.QueueOrderConfirmPayment,
            }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
