using System;

namespace Domain.Commands.Orders.Create
{
    public readonly struct CreateOrderCommandResponse
    {
        public required Guid Id { get; init; }
    }
}
