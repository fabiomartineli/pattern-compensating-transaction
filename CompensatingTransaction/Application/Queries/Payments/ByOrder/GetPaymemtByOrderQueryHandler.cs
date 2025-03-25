using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Queries.Payments;
using MediatR;

namespace Application.Queries.Paymemts.ByOrder
{
    public class GetPaymemtByOrderQueryHandler : IRequestHandler<GetPaymentByOrderQuery, GetPaymentByOrderQueryResponse>
    {
        private readonly IPaymentRepository _repository;

        public GetPaymemtByOrderQueryHandler(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetPaymentByOrderQueryResponse> Handle(GetPaymentByOrderQuery request, CancellationToken cancellationToken)
        {
            var payment = await _repository.GetByOrderAsync(request.OrderId, cancellationToken);
           
            if (payment == null)
                return default;
            
            return new()
            {
                Id = payment.Id,
                Status = payment.Status,
                OrderId = payment.OrderId,
            };
        }
    }
}
