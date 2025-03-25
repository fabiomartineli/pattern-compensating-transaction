using Application.Settings;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Saga;
using Domain.Commands.Deliveries.Create;
using Domain.Entities;
using Domain.Types;
using Domain.ValueObjects;
using Infra.MessageBus.Publisher;
using MediatR;
using Microsoft.Extensions.Options;
using System;

namespace Application.Commands.Deliveries.Create
{
    public class CreateDeliveryCommandHandler : ISaga<CreateDeliveryCommand>, IRequestHandler<CreateDeliveryCommand>
    {
        private readonly IDeliveryRepository _repository;
        private readonly IMessageBusPublisher _busPublisher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MessageBusDestinationSettings _messageBusDestination;

        public CreateDeliveryCommandHandler(IDeliveryRepository repository,
            IMessageBusPublisher busPublisher,
            IUnitOfWork unitOfWork,
            IOptions<MessageBusDestinationSettings> options)
        {
            _repository = repository;
            _busPublisher = busPublisher;
            _unitOfWork = unitOfWork;
            _messageBusDestination = options.Value;
        }

        public async Task CompensateTransactionAsync(CreateDeliveryCommand request, CancellationToken cancellationToken)
        {
            var delivery = await _repository.GetByOrderAsync(request.OrderId, cancellationToken);

            if (delivery is not null && delivery.Status == DeliveryStatus.InRouteProcess)
            {
                delivery.SetStatus(DeliveryStatus.Failed);

                await _unitOfWork.StartStranctionAsync(cancellationToken);
                await _repository.UpdateAsync(delivery, cancellationToken);
                await _unitOfWork.CommitTranscationAsync(cancellationToken);
            }

            await _busPublisher.ExecuteAsync(new MessageBusQueuePublisherRequest<CancellationOperationVO<Delivery>>
            {
                Content = new()
                {
                    CancellationData = delivery ?? new Delivery
                    {
                        OrderId = request.OrderId,
                    },
                    Note = "Ocorreu uma falha ao iniciar o processo de entrega do pedido.",
                },
                Id = Guid.CreateVersion7().ToString(),
                Destination = _messageBusDestination.QueueDeliveryFail,
            }, cancellationToken);
        }

        public async Task ExecuteTransactionAsync(CreateDeliveryCommand request, CancellationToken cancellationToken)
        {
            var delivery = await _repository.GetByOrderAsync(request.OrderId, cancellationToken);

            if (delivery == null)
            {
                delivery = new Delivery
                {
                    OrderId = request.OrderId,
                };

                await _unitOfWork.StartStranctionAsync(cancellationToken);
                await _repository.AddAsync(delivery, cancellationToken);
                await _unitOfWork.CommitTranscationAsync(cancellationToken);
            }
        }

        public async Task Handle(CreateDeliveryCommand request, CancellationToken cancellationToken)
        {
            await ExecuteTransactionAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
