using Application.Settings;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Saga;
using Domain.Commands.Payments.Fail;
using Domain.Entities;
using Domain.Types;
using Domain.ValueObjects;
using Infra.MessageBus.Publisher;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Commands.Payments.Fail
{
    public class FailPaymentCommandHandler : ISaga<FailPaymentCommand>, IRequestHandler<FailPaymentCommand>
    {
        private readonly IPaymentRepository _repository;
        private readonly IMessageBusPublisher _busPublisher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MessageBusDestinationSettings _messageBusDestination;

        public FailPaymentCommandHandler(IPaymentRepository repository,
            IMessageBusPublisher busPublisher,
            IUnitOfWork unitOfWork,
            IOptions<MessageBusDestinationSettings> options)
        {
            _repository = repository;
            _busPublisher = busPublisher;
            _unitOfWork = unitOfWork;
            _messageBusDestination = options.Value;
        }

        public async Task CompensateTransactionAsync(FailPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _repository.GetByOrderAsync(request.OrderId, cancellationToken);

            if (payment is not null && payment.Status == PaymentStatus.Failed)
            {
                payment.SetStatus(PaymentStatus.Processing);

                await _unitOfWork.StartStranctionAsync(cancellationToken);
                await _repository.UpdateAsync(payment, cancellationToken);
                await _unitOfWork.CommitTranscationAsync(cancellationToken);
            }
        }

        public async Task ExecuteTransactionAsync(FailPaymentCommand request, CancellationToken cancellationToken)
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
                Content = new()
                {
                    CancellationData = payment,
                    Note = "Pagamento não foi confirmado.",
                },
                Id = payment.Id.ToString(),
                Destination = _messageBusDestination.TopicPaymentFailed,
            }, cancellationToken);
        }

        public async Task Handle(FailPaymentCommand request, CancellationToken cancellationToken)
        {
            await ExecuteTransactionAsync(request, cancellationToken);
        }
    }
}
