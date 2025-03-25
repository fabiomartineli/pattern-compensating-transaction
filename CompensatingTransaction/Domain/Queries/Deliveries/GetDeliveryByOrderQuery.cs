using MediatR;
using System;

namespace Domain.Queries.Deliveries
{
    public readonly struct GetDeliveryByOrderQuery : IRequest<GetDeliveryByOrderQueryResponse>
    {
        public required Guid OrderId { get; init; }
    }
}
