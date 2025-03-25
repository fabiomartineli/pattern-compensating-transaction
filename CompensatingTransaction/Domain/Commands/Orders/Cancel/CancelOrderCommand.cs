using MediatR;

namespace Domain.Commands.Orders.Cancel
{
    public readonly struct CancelOrderCommand : IRequest
    {
        public CancelOrderInformation CancellationData { get; init; }
        public string Note { get; init; }
    }
}
