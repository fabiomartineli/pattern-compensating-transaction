using MediatR;
using System;

namespace Domain.Commands.Orders.ConfirmStock
{
    public readonly struct ConfirmStockOrderCommand : IRequest
    {
        public required Guid OrderId { get; init; }
    }
}
