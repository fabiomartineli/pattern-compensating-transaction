using MediatR;
using System;

namespace Domain.Commands.Orders.ConfirmDelivery
{
    public readonly struct ConfirmDeliveryOrderCommand : IRequest
    {
        public required Guid OrderId { get; init; }
    }
}
