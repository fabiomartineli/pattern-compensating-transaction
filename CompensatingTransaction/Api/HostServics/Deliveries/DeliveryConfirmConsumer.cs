using Application.Settings;
using Domain.Commands.Stocks.Create;
using Infra.MessageBus.Consumer;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Threading;
using Domain.Commands.Deliveries.Confirm;

namespace Api.HostServics.Deliveries
{
    public class DeliveryConfirmConsumer : IHostedService
    {
        private readonly IMessageBusConsumer _consumer;
        private readonly MessageBusDestinationSettings _messageBusDestination;

        public DeliveryConfirmConsumer(IMessageBusConsumer consumer,
            IOptions<MessageBusDestinationSettings> options)
        {
            _consumer = consumer;
            _messageBusDestination = options.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _consumer.ExecuteAsync<ConfirmDeliveryCommand>(new MessageBusConsumerRequest()
            {
                Destination = _messageBusDestination.QueueDeliveryConfirm,
            }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
