using Domain.Types;
using System;

namespace Domain.Queries.Stocks
{
    public record GetStockByOrderQueryResponse
    {
        public required Guid Id { get; init; }
        public required Guid OrderId { get; init; }
        public required StockStatus Status { get; init; }
    }
}
