using MediatR;

namespace Domain.Commands.Stocks.Cancel
{
    public readonly struct CancelStockCommand : IRequest
    {
        public CancelStockInformation CancellationData { get; init; }
        public string Note { get; init; }
    }
}
