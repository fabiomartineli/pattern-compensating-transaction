using MediatR;
using System;

namespace Domain.Queries.Stocks
{
    public readonly struct GetStockByOrderQuery : IRequest<GetStockByOrderQueryResponse>
    {
        public required Guid OrderId { get; init; }
    }
}
