using Application.Settings;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Saga;
using Domain.Commands.Payments.Request;
using Domain.Entities;
using Domain.Types;
using Domain.ValueObjects;
using Infra.MessageBus.Publisher;
using MediatR;
using Microsoft.Extensions.Options;
using System;

namespace Application.Commands.Payments.Request
{
    public class RequestPaymentCommandHandler : ISaga<RequestPaymentCommand>, IRequestHandler<RequestPaymentCommand>
    {
        private readonly IPaymentRepository _repository;
        private readonly IMessageBusPublisher _busPublisher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MessageBusDestinationSettings _messageBusDestination;

        public RequestPaymentCommandHandler(IPaymentRepository repository,
            IMessageBusPublisher busPublisher,
            IUnitOfWork unitOfWork,
            IOptions<MessageBusDestinationSettings> options)
        {
            _repository = repository;
            _busPublisher = busPublisher;
            _unitOfWork = unitOfWork;
            _messageBusDestination = options.Value;
        }

        public async Task CompensateTransactionAsync(RequestPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _repository.GetByOrderAsync(request.OrderId, cancellationToken);

            if (payment is not null && payment.Status == PaymentStatus.Processing)
            {
                payment.SetStatus(PaymentStatus.Failed);

                await _unitOfWork.StartStranctionAsync(cancellationToken);
                await _repository.UpdateAsync(payment, cancellationToken);
                await _unitOfWork.CommitTranscationAsync(cancellationToken);
            }

            await _busPublisher.ExecuteAsync(new MessageBusTopicPublisherRequest<CancellationOperationVO<Payment>>
            {
                Content = new CancellationOperationVO<Payment>
                {
                    CancellationData = payment ?? new Payment
                    {
                        OrderId = request.OrderId,
                    },
                    Note = "Não foi possível processar o pagamento."
                },
                Id = Guid.CreateVersion7().ToString(),
                Destination = _messageBusDestination.TopicPaymentFailed,
            }, cancellationToken);
        }

        public async Task ExecuteTransactionAsync(RequestPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _repository.GetByOrderAsync(request.OrderId, cancellationToken);

            if (payment == null)
            {
                payment = new Payment
                {
                    OrderId = request.OrderId,
                };

                await _unitOfWork.StartStranctionAsync(cancellationToken);
                await _repository.AddAsync(payment, cancellationToken);
                await _unitOfWork.CommitTranscationAsync(cancellationToken);
            }

            await _busPublisher.ExecuteAsync(new MessageBusTopicPublisherRequest<Payment>
            {
                Content = payment,
                Id = payment.Id.ToString(),
                Destination = _messageBusDestination.TopicPaymentRequested,
            }, cancellationToken);
        }

        public async Task Handle(RequestPaymentCommand request, CancellationToken cancellationToken)
        {
            await ExecuteTransactionAsync(request, cancellationToken);
        }
    }
}
