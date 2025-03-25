using Application.Settings;
using Domain.Commands.Orders.Cancel;
using Domain.Commands.Payments.Cancel;
using Infra.MessageBus.Consumer;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace Api.HostServics.Payments
{
    public class PaymentCancelConsumer : IHostedService
    {
        private readonly IMessageBusConsumer _consumer;
        private readonly MessageBusDestinationSettings _messageBusDestination;

        public PaymentCancelConsumer(IMessageBusConsumer consumer,
            IOptions<MessageBusDestinationSettings> options)
        {
            _consumer = consumer;
            _messageBusDestination = options.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _consumer.ExecuteAsync<CancelPaymentCommand>(new MessageBusConsumerRequest()
            {
                Destination = _messageBusDestination.QueuePaymentCancel,
            }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
