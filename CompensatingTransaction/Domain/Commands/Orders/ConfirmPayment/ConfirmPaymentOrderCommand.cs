using MediatR;
using System;

namespace Domain.Commands.Orders.ConfirmPayment
{
    public readonly struct ConfirmPaymentOrderCommand : IRequest
    {
        public required Guid OrderId { get; init; }
    }
}
