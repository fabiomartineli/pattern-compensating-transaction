using Application.Settings;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Saga;
using Domain.Commands.Orders.Create;
using Domain.Entities;
using Infra.MessageBus.Publisher;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Commands.Orders.Create
{
    public class CreateOrderCommandHandler : ISaga<CreateOrderCommand>, IRequestHandler<CreateOrderCommand>
    {
        private readonly IOrderRepository _repository;
        private readonly IMessageBusPublisher _busPublisher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MessageBusDestinationSettings _messageBusDestination;

        public CreateOrderCommandHandler(IOrderRepository repository,
            IMessageBusPublisher busPublisher,
            IUnitOfWork unitOfWork,
            IOptions<MessageBusDestinationSettings> options)
        {
            _repository = repository;
            _busPublisher = busPublisher;
            _unitOfWork = unitOfWork;
            _messageBusDestination = options.Value;
        }

        public Task CompensateTransactionAsync(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task ExecuteTransactionAsync(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetByNumberAsync(request.Number, cancellationToken);

            if (order == null)
            {
                order = new Order
                {
                    Id = request.Id,
                    Number = request.Number,
                };

                await _unitOfWork.StartStranctionAsync(cancellationToken);
                await _repository.AddAsync(order, cancellationToken);
                await _unitOfWork.CommitTranscationAsync(cancellationToken);
            }

            await _busPublisher.ExecuteAsync(new MessageBusTopicPublisherRequest<Order>
            {
                Content = order,
                Id = order.Id.ToString(),
                Destination = _messageBusDestination.TopicOrderCreated,
            }, cancellationToken);
        }

        public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
           await ExecuteTransactionAsync(request, cancellationToken);
        }
    }
}
