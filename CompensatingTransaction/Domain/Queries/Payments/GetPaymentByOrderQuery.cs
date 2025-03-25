using MediatR;
using System;

namespace Domain.Queries.Payments
{
    public readonly struct GetPaymentByOrderQuery : IRequest<GetPaymentByOrderQueryResponse>
    {
        public required Guid OrderId { get; init; }
    }
}
