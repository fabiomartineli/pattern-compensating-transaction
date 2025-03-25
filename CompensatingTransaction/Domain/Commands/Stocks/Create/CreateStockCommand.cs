using MediatR;
using System;

namespace Domain.Commands.Stocks.Create
{
    public readonly struct CreateStockCommand : IRequest
    {
        public Guid OrderId { get; init; }
    }
}
