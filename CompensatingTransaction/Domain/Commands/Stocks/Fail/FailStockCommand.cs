using MediatR;
using System;

namespace Domain.Commands.Stocks.Fail
{
    public readonly struct FailStockCommand : IRequest
    {
        public Guid Id { get; init; }
    }
}
