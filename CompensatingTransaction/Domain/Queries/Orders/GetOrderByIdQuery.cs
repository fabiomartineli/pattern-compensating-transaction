using MediatR;
using System;

namespace Domain.Queries.Orders
{
    public readonly struct GetOrderByIdQuery : IRequest<GetOrderByIdQueryResponse>
    {
        public Guid Id { get; init; }
    }
}
