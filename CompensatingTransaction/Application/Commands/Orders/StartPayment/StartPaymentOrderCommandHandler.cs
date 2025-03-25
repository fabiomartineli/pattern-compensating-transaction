using Domain.Abstractions.Repositories;
using Domain.Abstractions.Saga;
using Domain.Commands.Orders.StartPayment;
using Domain.Types;
using MediatR;

namespace Application.Commands.Orders.PaymentProcessing
{
    public class StartPaymentOrderCommandHandler : ISaga<StartPaymentOrderCommand>, IRequestHandler<StartPaymentOrderCommand>
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public StartPaymentOrderCommandHandler(IOrderRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public Task CompensateTransactionAsync(StartPaymentOrderCommand request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task ExecuteTransactionAsync(StartPaymentOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(request.OrderId, cancellationToken);

            if (order is not null && order.Status == OrderStatus.Created)
            {
                order.SetStatus(OrderStatus.InPaymentProcess);

                await _unitOfWork.StartStranctionAsync(cancellationToken);
                await _repository.UpdateAsync(order, cancellationToken);
                await _unitOfWork.CommitTranscationAsync(cancellationToken);
            }
        }

        public async Task Handle(StartPaymentOrderCommand request, CancellationToken cancellationToken)
        {
            await ExecuteTransactionAsync(request, cancellationToken);
        }
    }
}
