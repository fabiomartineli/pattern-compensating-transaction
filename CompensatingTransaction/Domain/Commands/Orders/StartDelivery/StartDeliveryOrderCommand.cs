using MediatR;
using System;

namespace Domain.Commands.Orders.StartDelivery
{
    public readonly struct StartDeliveryOrderCommand : IRequest
    {
        public Guid OrderId { get; init; }
    }
}
