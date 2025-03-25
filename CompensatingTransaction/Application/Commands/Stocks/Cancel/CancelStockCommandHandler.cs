using Application.Settings;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Saga;
using Domain.Commands.Stocks.Cancel;
using Domain.Entities;
using Domain.Types;
using Domain.ValueObjects;
using Infra.MessageBus.Publisher;
using MediatR;
using Microsoft.Extensions.Options;
using System;

namespace Application.Commands.Stocks.Cancel
{
    public class CancelStockCommandHandler : ISaga<CancelStockCommand>, IRequestHandler<CancelStockCommand>
    {
        private readonly IStockOrderRepository _stockOrderRepository;
        private readonly IMessageBusPublisher _busPublisher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MessageBusDestinationSettings _messageBusDestination;

        public CancelStockCommandHandler(IStockOrderRepository stockOrderRepository,
            IMessageBusPublisher busPublisher,
            IUnitOfWork unitOfWork,
            IOptions<MessageBusDestinationSettings> options)
        {
            _busPublisher = busPublisher;
            _unitOfWork = unitOfWork;
            _messageBusDestination = options.Value;
            _stockOrderRepository = stockOrderRepository;
        }

        public async Task CompensateTransactionAsync(CancelStockCommand request, CancellationToken cancellationToken)
        {
            var stockOrder = await _stockOrderRepository.GetByOrderAsync(request.CancellationData.OrderId, cancellationToken);

            if (stockOrder is not null && stockOrder.Status == StockStatus.WaitingReturn)
            {
                stockOrder.SetStatus(StockStatus.Confirmed);

                await _unitOfWork.StartStranctionAsync(cancellationToken);
                await _stockOrderRepository.UpdateAsync(stockOrder, cancellationToken);
                await _unitOfWork.CommitTranscationAsync(cancellationToken);
            }
        }

        public async Task ExecuteTransactionAsync(CancelStockCommand request, CancellationToken cancellationToken)
        {
            var stockOrder = await _stockOrderRepository.GetByOrderAsync(request.CancellationData.OrderId, cancellationToken);

            if (stockOrder is not null && stockOrder.Status == StockStatus.Confirmed)
            {
                stockOrder.SetStatus(StockStatus.WaitingReturn);

                await _unitOfWork.StartStranctionAsync(cancellationToken);
                await _stockOrderRepository.UpdateAsync(stockOrder, cancellationToken);
                await _unitOfWork.CommitTranscationAsync(cancellationToken);
            }

            await _busPublisher.ExecuteAsync(new MessageBusTopicPublisherRequest<CancellationOperationVO<StockOrder>>
            {
                Content = new()
                {
                    CancellationData = stockOrder,
                    Note = request.Note,
                },
                Id = stockOrder.Id.ToString(),
                Destination = _messageBusDestination.TopicStockFailed,
            }, cancellationToken);
        }

        public async Task Handle(CancelStockCommand request, CancellationToken cancellationToken)
        {
            await ExecuteTransactionAsync(request, cancellationToken);
        }
    }
}
