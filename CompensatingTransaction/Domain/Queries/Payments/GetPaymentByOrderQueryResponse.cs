using Domain.Types;
using System;

namespace Domain.Queries.Payments
{
    public record GetPaymentByOrderQueryResponse
    {
        public required Guid Id { get; init; }
        public required Guid OrderId { get; init; }
        public required PaymentStatus Status { get; init; }
    }
}
