using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Queries.Deliveries;
using MediatR;

namespace Application.Queries.Deliveries.ByOrder
{
    public class GetDeliveryByOrderQueryHandler : IRequestHandler<GetDeliveryByOrderQuery, GetDeliveryByOrderQueryResponse>
    {
        private readonly IDeliveryRepository _repository;

        public GetDeliveryByOrderQueryHandler(IDeliveryRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetDeliveryByOrderQueryResponse> Handle(GetDeliveryByOrderQuery request, CancellationToken cancellationToken)
        {
            var delivery = await _repository.GetByOrderAsync(request.OrderId, cancellationToken);

            if (delivery == null)
                return default;

            return new()
            {
                Id = delivery.Id,
                OrderId = delivery.OrderId,
                Status = delivery.Status,
                UpdatedAt = delivery.UpdatedAt,
            };
        }
    }
}
