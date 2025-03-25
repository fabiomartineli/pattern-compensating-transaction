using Application.Settings;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Saga;
using Domain.Commands.Stocks.Create;
using Domain.Entities;
using Domain.Types;
using Domain.ValueObjects;
using Infra.MessageBus.Publisher;
using MediatR;
using Microsoft.Extensions.Options;
using System;

namespace Application.Commands.Stocks.Create
{
    public class CreateStockCommandHandler : ISaga<CreateStockCommand>, IRequestHandler<CreateStockCommand>
    {
        private readonly IStockRepository _repository;
        private readonly IOrderRepository _orderRepository;
        private readonly IStockOrderRepository _stockOrderRepository;
        private readonly IMessageBusPublisher _busPublisher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MessageBusDestinationSettings _messageBusDestination;

        public CreateStockCommandHandler(IStockRepository repository,
            IMessageBusPublisher busPublisher,
            IUnitOfWork unitOfWork,
            IOptions<MessageBusDestinationSettings> options,
            IOrderRepository orderRepository,
            IStockOrderRepository stockOrderRepository)
        {
            _repository = repository;
            _busPublisher = busPublisher;
            _unitOfWork = unitOfWork;
            _messageBusDestination = options.Value;
            _orderRepository = orderRepository;
            _stockOrderRepository = stockOrderRepository;
        }

        public async Task CompensateTransactionAsync(CreateStockCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            var stock = await _repository.GetByProductAsync(order.ProductId, cancellationToken);
            var stockOrder = await _stockOrderRepository.GetByOrderAsync(request.OrderId, cancellationToken);

            if (stockOrder is not null && stockOrder.Status == StockStatus.Confirmed)
            {
                stockOrder.SetStatus(StockStatus.Failed);
                stock.Increase();

                await _unitOfWork.StartStranctionAsync(cancellationToken);
                await _stockOrderRepository.UpdateAsync(stockOrder, cancellationToken);
                await _repository.UpdateAsync(stock, cancellationToken);
                await _unitOfWork.CommitTranscationAsync(cancellationToken);
            }

            await _busPublisher.ExecuteAsync(new MessageBusTopicPublisherRequest<CancellationOperationVO<StockOrder>>
            {
                Content = new()
                {
                    CancellationData = new StockOrder
                    {
                        OrderId = request.OrderId,
                    },
                    Note = "Produto solicitado está em falta no estoque.",
                },
                Id = Guid.CreateVersion7().ToString(),
                Destination = _messageBusDestination.TopicStockFailed,
            }, cancellationToken);
        }

        public async Task ExecuteTransactionAsync(CreateStockCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            var stockOrder = await _stockOrderRepository.GetByOrderAsync(request.OrderId, cancellationToken);

            if (stockOrder == null)
            {
                stockOrder = new StockOrder
                {
                    OrderId = request.OrderId,
                };

                stockOrder.SetStatus(StockStatus.Confirmed);

                await _unitOfWork.StartStranctionAsync(cancellationToken);
                await _stockOrderRepository.AddAsync(stockOrder, cancellationToken);
                await _unitOfWork.CommitTranscationAsync(cancellationToken);
            }

            await _busPublisher.ExecuteAsync(new MessageBusTopicPublisherRequest<StockOrder>
            {
                Content = stockOrder,
                Id = stockOrder.Id.ToString(),
                Destination = _messageBusDestination.TopicStockConfirmed,
            }, cancellationToken);
        }

        public async Task Handle(CreateStockCommand request, CancellationToken cancellationToken)
        {
            await ExecuteTransactionAsync(request, cancellationToken);
        }
    }
}
