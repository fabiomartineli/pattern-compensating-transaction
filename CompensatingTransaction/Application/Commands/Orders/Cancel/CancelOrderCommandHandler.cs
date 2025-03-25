using Domain.Abstractions.Repositories;
using Domain.Abstractions.Saga;
using Domain.Commands.Orders.Cancel;
using Domain.Types;
using MediatR;

namespace Application.Commands.Orders.Cancel
{
    public class CancelOrderCommandHandler : ISaga<CancelOrderCommand>, IRequestHandler<CancelOrderCommand>
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelOrderCommandHandler(IOrderRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public Task CompensateTransactionAsync(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task ExecuteTransactionAsync(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(request.CancellationData.OrderId, cancellationToken);

            if (order is not null && order.Status != OrderStatus.Failed)
            {
                order.SetStatus(OrderStatus.Failed);
                order.SetNote(request.Note);

                await _unitOfWork.StartStranctionAsync(cancellationToken);
                await _repository.UpdateAsync(order, cancellationToken);
                await _unitOfWork.CommitTranscationAsync(cancellationToken);
            }
        }

        public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            await ExecuteTransactionAsync(request, cancellationToken);
        }
    }
}
