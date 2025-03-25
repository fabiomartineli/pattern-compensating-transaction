using Domain.Abstractions.Repositories;
using Domain.Abstractions.Saga;
using Domain.Commands.Orders.ConfirmPayment;
using Domain.Types;
using MediatR;

namespace Application.Commands.Orders.ConfirmPayment
{
    public class ConfirmPaymentOrderCommandHandler : ISaga<ConfirmPaymentOrderCommand>, IRequestHandler<ConfirmPaymentOrderCommand>
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmPaymentOrderCommandHandler(IOrderRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public Task CompensateTransactionAsync(ConfirmPaymentOrderCommand request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task ExecuteTransactionAsync(ConfirmPaymentOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(request.OrderId, cancellationToken);

            if (order is not null && order.Status == OrderStatus.InPaymentProcess)
            {
                order.SetStatus(OrderStatus.InStockSeparationProcess);

                await _unitOfWork.StartStranctionAsync(cancellationToken);
                await _repository.UpdateAsync(order, cancellationToken);
                await _unitOfWork.CommitTranscationAsync(cancellationToken);
            }
        }

        public async Task Handle(ConfirmPaymentOrderCommand request, CancellationToken cancellationToken)
        {
            await ExecuteTransactionAsync(request, cancellationToken);
        }
    }
}
