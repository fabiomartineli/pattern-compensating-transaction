using Domain.Abstractions.Repositories;
using Domain.Queries.Orders;
using MediatR;

namespace Application.Queries.Orders.GetOrder
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, GetOrderByIdQueryResponse>
    {
        private readonly IOrderRepository _repository;

        public GetOrderByIdQueryHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetOrderByIdQueryResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (order == null)
                return default;

            return new()
            {
                Id = order.Id,
                Number = order.Number,
                Status = order.Status,
                UpdatedAt = order.UpdatedAt,
                Note = order.Note,
            };
        }
    }
}
