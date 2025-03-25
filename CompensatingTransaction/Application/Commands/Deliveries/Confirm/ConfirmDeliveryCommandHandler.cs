using Application.Settings;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Saga;
using Domain.Commands.Deliveries.Confirm;
using Domain.Entities;
using Domain.Types;
using Infra.MessageBus.Publisher;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Commands.Deliveries.Confirm
{
    public class ConfirmDeliveryCommandHandler : ISaga<ConfirmDeliveryCommand>, IRequestHandler<ConfirmDeliveryCommand>
    {
        private readonly IDeliveryRepository _repository;
        private readonly IMessageBusPublisher _busPublisher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MessageBusDestinationSettings _messageBusDestination;

        public ConfirmDeliveryCommandHandler(IDeliveryRepository repository,
            IMessageBusPublisher busPublisher,
            IUnitOfWork unitOfWork,
            IOptions<MessageBusDestinationSettings> options)
        {
            _repository = repository;
            _busPublisher = busPublisher;
            _unitOfWork = unitOfWork;
            _messageBusDestination = options.Value;
        }

        public async Task CompensateTransactionAsync(ConfirmDeliveryCommand request, CancellationToken cancellationToken)
        {
            var delivery = await _repository.GetByOrderAsync(request.OrderId, cancellationToken);

            if (delivery is not null && delivery.Status == DeliveryStatus.Delivered)
            {
                delivery.SetStatus(DeliveryStatus.InRouteProcess);

                await _unitOfWork.StartStranctionAsync(cancellationToken);
                await _repository.UpdateAsync(delivery, cancellationToken);
                await _unitOfWork.CommitTranscationAsync(cancellationToken);
            }
        }

        public async Task ExecuteTransactionAsync(ConfirmDeliveryCommand request, CancellationToken cancellationToken)
        {
            var delivery = await _repository.GetByOrderAsync(request.OrderId, cancellationToken);

            if (delivery is not null && delivery.Status == DeliveryStatus.InRouteProcess)
            {
                delivery.SetStatus(DeliveryStatus.Delivered);

                await _unitOfWork.StartStranctionAsync(cancellationToken);
                await _repository.UpdateAsync(delivery, cancellationToken);
                await _unitOfWork.CommitTranscationAsync(cancellationToken);
            }

            await _busPublisher.ExecuteAsync(new MessageBusTopicPublisherRequest<Delivery>
            {
                Content = delivery,
                Id = delivery.Id.ToString(),
                Destination = _messageBusDestination.TopicDeliveryConfirmed,
            }, cancellationToken);
        }

        public async Task Handle(ConfirmDeliveryCommand request, CancellationToken cancellationToken)
        {
            await ExecuteTransactionAsync(request, cancellationToken);
        }
    }
}
