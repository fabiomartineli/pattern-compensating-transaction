using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Domain.Commands.Payments.Request
{
    public readonly struct RequestPaymentCommand : IRequest
    {
        [JsonPropertyName("Id")] public Guid OrderId { get; init; }
    }
}
