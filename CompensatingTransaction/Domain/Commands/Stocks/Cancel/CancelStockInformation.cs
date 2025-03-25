using System;

namespace Domain.Commands.Stocks.Cancel
{
    public readonly struct CancelStockInformation
    {
        public Guid OrderId { get; init; }
    }
}
