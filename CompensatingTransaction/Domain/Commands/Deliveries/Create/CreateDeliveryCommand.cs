using MediatR;
using System;

namespace Domain.Commands.Deliveries.Create
{
    public readonly struct CreateDeliveryCommand : IRequest
    {
        public Guid OrderId { get; init; }
    }
}
