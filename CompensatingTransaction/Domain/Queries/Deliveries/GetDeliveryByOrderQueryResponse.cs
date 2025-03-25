using Domain.Types;
using System;

namespace Domain.Queries.Deliveries
{
    public record GetDeliveryByOrderQueryResponse
    {
        public required Guid Id { get; init; }
        public required Guid OrderId { get; init; }
        public required DateTime UpdatedAt { get; init; }
        public required DeliveryStatus Status { get; init; }
    }
}
