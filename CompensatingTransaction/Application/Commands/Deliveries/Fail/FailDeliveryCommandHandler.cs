using Application.Settings;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Saga;
using Domain.Commands.Deliveries.Fail;
using Domain.Entities;
using Domain.Types;
using Domain.ValueObjects;
using Infra.MessageBus.Publisher;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Commands.Deliveries.Fail
{
    public class FailDeliveryCommandHandler : ISaga<FailDeliveryCommand>, IRequestHandler<FailDeliveryCommand>
    {
        private readonly IDeliveryRepository _repository;
        private readonly IMessageBusPublisher _busPublisher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MessageBusDestinationSettings _messageBusDestination;

        public FailDeliveryCommandHandler(IDeliveryRepository repository,
            IMessageBusPublisher busPublisher,
            IUnitOfWork unitOfWork,
            IOptions<MessageBusDestinationSettings> options)
        {
            _repository = repository;
            _busPublisher = busPublisher;
            _unitOfWork = unitOfWork;
            _messageBusDestination = options.Value;
        }

        public async Task CompensateTransactionAsync(FailDeliveryCommand request, CancellationToken cancellationToken)
        {
            var delivery = await _repository.GetByOrderAsync(request.OrderId, cancellationToken);

            if (delivery is not null && delivery.Status == DeliveryStatus.Failed)
            {
                delivery.SetStatus(DeliveryStatus.InRouteProcess);

                await _unitOfWork.StartStranctionAsync(cancellationToken);
                await _repository.UpdateAsync(delivery, cancellationToken);
                await _unitOfWork.CommitTranscationAsync(cancellationToken);
            }
        }

        public async Task ExecuteTransactionAsync(FailDeliveryCommand request, CancellationToken cancellationToken)
        {
            var delivery = await _repository.GetByOrderAsync(request.OrderId, cancellationToken);

            if (delivery is not null && delivery.Status == DeliveryStatus.InRouteProcess)
            {
                delivery.SetStatus(DeliveryStatus.Failed);

                await _unitOfWork.StartStranctionAsync(cancellationToken);
                await _repository.UpdateAsync(delivery, cancellationToken);
                await _unitOfWork.CommitTranscationAsync(cancellationToken);
            }

            await _busPublisher.ExecuteAsync(new MessageBusTopicPublisherRequest<CancellationOperationVO<Delivery>>
            {
                Content = new()
                {
                    CancellationData = delivery,
                    Note = "Falha na entrega do produto.",
                },
                Id = delivery.Id.ToString(),
                Destination = _messageBusDestination.TopicDeliveryFailed,
            }, cancellationToken);
        }

        public async Task Handle(FailDeliveryCommand request, CancellationToken cancellationToken)
        {
           await ExecuteTransactionAsync(request, cancellationToken);
        }
    }
}
