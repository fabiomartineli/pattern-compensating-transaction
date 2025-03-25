using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Queries.Stocks;
using MediatR;

namespace Application.Queries.Stocks.ByOrder
{
    public class GetStockByOrderQueryHandler : IRequestHandler<GetStockByOrderQuery, GetStockByOrderQueryResponse>
    {
        private readonly IStockOrderRepository _repository;

        public GetStockByOrderQueryHandler(IStockOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetStockByOrderQueryResponse> Handle(GetStockByOrderQuery request, CancellationToken cancellationToken)
        {
            var stock = await _repository.GetByOrderAsync(request.OrderId, cancellationToken);

            if (stock == null)
                return default;

            return new()
            {
                Id = stock.Id,
                Status = stock.Status,
                OrderId = stock.OrderId,
            };
        }
    }
}
