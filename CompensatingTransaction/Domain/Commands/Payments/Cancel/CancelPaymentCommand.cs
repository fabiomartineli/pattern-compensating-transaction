using MediatR;

namespace Domain.Commands.Payments.Cancel
{
    public readonly struct CancelPaymentCommand : IRequest
    {
        public CancelPaymentInformation CancellationData { get; init; }
        public string Note { get; init; }
    }
}
