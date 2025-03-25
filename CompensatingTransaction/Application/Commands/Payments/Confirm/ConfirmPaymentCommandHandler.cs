using Application.Settings;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Saga;
using Domain.Commands.Payments.Confirm;
using Domain.Entities;
using Domain.Types;
using Domain.ValueObjects;
using Infra.MessageBus.Publisher;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Commands.Payments.Confirm
{
    public class ConfirmPaymentCommandHandler : ISaga<ConfirmPaymentCommand>, IRequestHandler<ConfirmPaymentCommand>
    {
        private readonly IPaymentRepository _repository;
        private readonly IMessageBusPublisher _busPublisher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MessageBusDestinationSettings _messageBusDestination;

        public ConfirmPaymentCommandHandler(IPaymentRepository repository,
            IMessageBusPublisher busPublisher,
            IUnitOfWork unitOfWork,
            IOptions<MessageBusDestinationSettings> options)
        {
            _repository = repository;
            _busPublisher = busPublisher;
            _unitOfWork = unitOfWork;
            _messageBusDestination = options.Value;
        }

        public async Task CompensateTransactionAsync(ConfirmPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _repository.GetByOrderAsync(request.OrderId, cancellationToken);

            if (payment is not null && payment.Status == PaymentStatus.Success)
            {
                payment.SetStatus(PaymentStatus.Processing);

                await _unitOfWork.StartStranctionAsync(cancellationToken);
                await _repository.UpdateAsync(payment, cancellationToken);
                await _unitOfWork.CommitTranscationAsync(cancellationToken);
            }

            await _busPublisher.ExecuteAsync(new MessageBusQueuePublisherRequest<CancellationOperationVO<Payment>>
            {
                Content = new()
                {
                    CancellationData = payment,
                    Note = "Pagamento não foi concluído com sucesso.",
                },
                Id = payment.Id.ToString(),
                Destination = _messageBusDestination.QueuePaymentCancel,
            }, cancellationToken);
        }

        public async Task ExecuteTransactionAsync(ConfirmPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _repository.GetByOrderAsync(request.OrderId, cancellationToken);

            if (payment is not null && payment.Status == PaymentStatus.Processing)
            {
                payment.SetStatus(PaymentStatus.Success);

                await _unitOfWork.StartStranctionAsync(cancellationToken);
                await _repository.UpdateAsync(payment, cancellationToken);
                await _unitOfWork.CommitTranscationAsync(cancellationToken);
            }

            await _busPublisher.ExecuteAsync(new MessageBusTopicPublisherRequest<Payment>
            {
                Content = payment,
                Id = payment.Id.ToString(),
                Destination = _messageBusDestination.TopicPaymentConfirmed,
            }, cancellationToken);
        }

        public async Task Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
        {
            await ExecuteTransactionAsync(request, cancellationToken);
        }
    }
}
