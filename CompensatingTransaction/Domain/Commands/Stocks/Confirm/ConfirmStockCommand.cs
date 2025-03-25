using MediatR;
using System;

namespace Domain.Commands.Stocks.Confirm
{
    public readonly struct ConfirmStockCommand : IRequest
    {
        public Guid Id { get; init; }
    }
}
