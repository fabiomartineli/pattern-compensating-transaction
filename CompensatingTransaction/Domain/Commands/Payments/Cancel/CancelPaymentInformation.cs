using System;

namespace Domain.Commands.Payments.Cancel
{
    public readonly struct CancelPaymentInformation
    {
        public Guid OrderId { get; init; }
    }
}
