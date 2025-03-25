using MediatR;
using System;

namespace Domain.Commands.Payments.Confirm
{
    public readonly partial struct ConfirmPaymentCommand : IRequest
    {
        public Guid OrderId { get; init; }
    }
}
