using MediatR;
using System;

namespace Domain.Commands.Orders.StartPayment
{
    public readonly struct StartPaymentOrderCommand : IRequest
    {
        public required Guid OrderId { get; init; }
    }
}
