using Domain.Abstractions.Repositories;
using Domain.Abstractions.Saga;
using Domain.Commands.Orders.ConfirmStock;
using Domain.Types;
using MediatR;

namespace Application.Commands.Orders.ConfirmStock
{
    public class ConfirmStockOrderCommandHandler : ISaga<ConfirmStockOrderCommand>, IRequestHandler<ConfirmStockOrderCommand>
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmStockOrderCommandHandler(IOrderRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public Task CompensateTransactionAsync(ConfirmStockOrderCommand request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task ExecuteTransactionAsync(ConfirmStockOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(request.OrderId, cancellationToken);

            if (order is not null && order.Status == OrderStatus.InStockSeparationProcess)
            {
                order.SetStatus(OrderStatus.InDeliveryProcess);

                await _unitOfWork.StartStranctionAsync(cancellationToken);
                await _repository.UpdateAsync(order, cancellationToken);
                await _unitOfWork.CommitTranscationAsync(cancellationToken);
            }
        }

        public async Task Handle(ConfirmStockOrderCommand request, CancellationToken cancellationToken)
        {
           await ExecuteTransactionAsync(request, cancellationToken);
        }
    }
}
