using MediatR;
using System;

namespace Domain.Commands.Deliveries.Confirm
{
    public readonly struct ConfirmDeliveryCommand : IRequest
    {
        public Guid OrderId { get; init; }
    }
}
