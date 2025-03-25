using System;

namespace Domain.Commands.Orders.Cancel
{
    public readonly struct CancelOrderInformation
    {
        public Guid OrderId { get; init; }
    }
}
