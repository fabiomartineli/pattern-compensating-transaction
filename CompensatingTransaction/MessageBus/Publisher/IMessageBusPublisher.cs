using System.Threading;
using System.Threading.Tasks;

namespace Infra.MessageBus.Publisher
{
    public interface IMessageBusPublisher
    {
        Task ExecuteAsync<TContent>(MessageBusQueuePublisherRequest<TContent> request, CancellationToken cancellationToken);
        Task ExecuteAsync<TContent>(MessageBusTopicPublisherRequest<TContent> request, CancellationToken cancellationToken);
    }
}
