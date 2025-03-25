using MediatR;
using System;

namespace Domain.Commands.Orders.Create
{
    public readonly struct CreateOrderCommand : IRequest
    {
        public required string Number { get; init; }
        public Guid Id { get; init; }

        public CreateOrderCommand()
        {
            Id = Guid.CreateVersion7();
        }
    }
}
