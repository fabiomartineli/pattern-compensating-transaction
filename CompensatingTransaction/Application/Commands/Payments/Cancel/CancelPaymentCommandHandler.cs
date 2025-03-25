using Application.Settings;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Saga;
using Domain.Commands.Payments.Cancel;
using Domain.Entities;
using Domain.Types;
using Domain.ValueObjects;
using Infra.MessageBus.Publisher;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Commands.Payments.Cancel
{
    public class CancelPaymentCommandHandler : ISaga<CancelPaymentCommand>, IRequestHandler<CancelPaymentCommand>
    {
        private readonly IPaymentRepository _repository;
        private readonly IMessageBusPublisher _busPublisher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MessageBusDestinationSettings _messageBusDestination;

        public CancelPaymentCommandHandler(IPaymentRepository repository,
            IMessageBusPublisher busPublisher,
            IUnitOfWork unitOfWork,
            IOptions<MessageBusDestinationSettings> options)
        {
            _repository = repository;
            _busPublisher = busPublisher;
            _unitOfWork = unitOfWork;
            _messageBusDestination = options.Value;
        }

        public async Task CompensateTransactionAsync(CancelPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _repository.GetByOrderAsync(request.CancellationData.OrderId, cancellationToken);

            if (payment is not null && payment.Status == PaymentStatus.RefundRequested)
            {
                payment.SetStatus(PaymentStatus.Success);

                await _unitOfWork.StartStranctionAsync(cancellationToken);
                await _repository.UpdateAsync(payment, cancellationToken);
                await _unitOfWork.CommitTranscationAsync(cancellationToken);
            }
        }

        public async Task ExecuteTransactionAsync(CancelPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _repository.GetByOrderAsync(request.CancellationData.OrderId, cancellationToken);

            if (payment is not null && payment.Status == PaymentStatus.Success)
            {
                payment.SetStatus(PaymentStatus.RefundRequested);

                await _unitOfWork.StartStranctionAsync(cancellationToken);
                await _repository.UpdateAsync(payment, cancellationToken);
                await _unitOfWork.CommitTranscationAsync(cancellationToken);
            }

            await _busPublisher.ExecuteAsync(new MessageBusTopicPublisherRequest<CancellationOperationVO<Payment>>
            {
                Content = new()
                {
                    CancellationData = payment,
                    Note = request.Note,
                },
                Id = payment.Id.ToString(),
                Destination = _messageBusDestination.TopicPaymentFailed,
            }, cancellationToken);
        }

        public async Task Handle(CancelPaymentCommand request, CancellationToken cancellationToken)
        {
            await ExecuteTransactionAsync(request, cancellationToken);
        }
    }
}
