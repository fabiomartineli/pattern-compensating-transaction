using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Infra.MessageBus.Consumer
{
    public interface IMessageBusConsumer
    {
        Task ExecuteAsync<TContent>(MessageBusConsumerRequest request, CancellationToken cancellationToken) where TContent : IRequest;
    }
}
