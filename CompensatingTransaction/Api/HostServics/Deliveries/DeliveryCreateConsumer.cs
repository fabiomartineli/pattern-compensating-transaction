using Application.Settings;
using Domain.Commands.Stocks.Create;
using Infra.MessageBus.Consumer;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Threading;
using Domain.Commands.Deliveries.Create;

namespace Api.HostServics.Deliveries
{
    public class DeliveryCreateConsumer : IHostedService
    {
        private readonly IMessageBusConsumer _consumer;
        private readonly MessageBusDestinationSettings _messageBusDestination;

        public DeliveryCreateConsumer(IMessageBusConsumer consumer,
            IOptions<MessageBusDestinationSettings> options)
        {
            _consumer = consumer;
            _messageBusDestination = options.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _consumer.ExecuteAsync<CreateDeliveryCommand>(new MessageBusConsumerRequest()
            {
                Destination = _messageBusDestination.QueueDeliveryCreate,
            }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
