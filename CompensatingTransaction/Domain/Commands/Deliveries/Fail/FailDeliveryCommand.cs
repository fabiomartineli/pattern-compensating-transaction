using MediatR;
using System;

namespace Domain.Commands.Deliveries.Fail
{
    public readonly struct FailDeliveryCommand : IRequest
    {
        public Guid OrderId { get; init; }
    }
}
