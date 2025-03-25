using Domain.Types;
using System;

namespace Domain.Queries.Orders
{
    public record GetOrderByIdQueryResponse
    {
        public required Guid Id { get; init; }
        public required string Number { get; init; }
        public required string Note { get; init; }
        public required DateTime UpdatedAt { get; init; }
        public required OrderStatus Status { get; init; }
    }
}
