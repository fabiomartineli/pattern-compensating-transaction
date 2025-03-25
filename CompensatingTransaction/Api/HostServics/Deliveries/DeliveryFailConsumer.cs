using Application.Settings;
using Domain.Commands.Stocks.Create;
using Infra.MessageBus.Consumer;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Threading;
using Domain.Commands.Deliveries.Fail;

namespace Api.HostServics.Deliveries
{
    public class DeliveryFailConsumer : IHostedService
    {
        private readonly IMessageBusConsumer _consumer;
        private readonly MessageBusDestinationSettings _messageBusDestination;

        public DeliveryFailConsumer(IMessageBusConsumer consumer,
            IOptions<MessageBusDestinationSettings> options)
        {
            _consumer = consumer;
            _messageBusDestination = options.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _consumer.ExecuteAsync<FailDeliveryCommand>(new MessageBusConsumerRequest()
            {
                Destination = _messageBusDestination.QueueDeliveryFail,
            }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
