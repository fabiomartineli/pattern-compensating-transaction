using Domain.Abstractions.Repositories;
using Domain.Abstractions.Saga;
using Domain.Commands.Orders.ConfirmDelivery;
using Domain.Types;
using MediatR;

namespace Application.Commands.Orders.ConfirmDelivery
{
    public class ConfirmDeliveryOrderCommandHandler : ISaga<ConfirmDeliveryOrderCommand>, IRequestHandler<ConfirmDeliveryOrderCommand>
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmDeliveryOrderCommandHandler(IOrderRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public Task CompensateTransactionAsync(ConfirmDeliveryOrderCommand request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task ExecuteTransactionAsync(ConfirmDeliveryOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(request.OrderId, cancellationToken);

            if (order is not null && order.Status == OrderStatus.InDeliveryProcess)
            {
                order.SetStatus(OrderStatus.DeliveryConfirmed);

                await _unitOfWork.StartStranctionAsync(cancellationToken);
                await _repository.UpdateAsync(order, cancellationToken);
                await _unitOfWork.CommitTranscationAsync(cancellationToken);
            }
        }

        public async Task Handle(ConfirmDeliveryOrderCommand request, CancellationToken cancellationToken)
        {
           await ExecuteTransactionAsync(request, cancellationToken);
        }
    }
}
