using MediatR;
using System;

namespace Domain.Commands.Payments.Fail
{
    public readonly struct FailPaymentCommand : IRequest
    {
        public Guid OrderId { get; init; }
    }
}
