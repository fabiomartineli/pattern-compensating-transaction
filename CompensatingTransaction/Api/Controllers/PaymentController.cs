using Application.Settings;
using Domain.Commands.Payments.Confirm;
using Domain.Commands.Payments.Fail;
using Domain.Queries.Payments;
using Infra.MessageBus.Publisher;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly IMessageBusPublisher _messageBusPublisher;
        private readonly IMediator _mediator;
        private readonly MessageBusDestinationSettings _messageBusDestination;

        public PaymentController(IMessageBusPublisher messageBusPublisher,
            IOptions<MessageBusDestinationSettings> options,
            IMediator mediator)
        {
            _messageBusPublisher = messageBusPublisher;
            _messageBusDestination = options.Value;
            _mediator = mediator;
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> Confirm([FromBody] ConfirmPaymentCommand request, CancellationToken cancellationToken)
        {
            await _messageBusPublisher.ExecuteAsync(new MessageBusQueuePublisherRequest<ConfirmPaymentCommand>
            {
                Content = request,
                Destination = _messageBusDestination.QueuePaymentConfirm,
                Id = Guid.CreateVersion7().ToString(),
            }, cancellationToken);

            return Accepted();
        }

        [HttpPost("fail")]
        public async Task<IActionResult> Fail([FromBody] FailPaymentCommand request, CancellationToken cancellationToken)
        {
            await _messageBusPublisher.ExecuteAsync(new MessageBusQueuePublisherRequest<FailPaymentCommand>
            {
                Content = request,
                Destination = _messageBusDestination.QueuePaymentFail,
                Id = Guid.CreateVersion7().ToString(),
            }, cancellationToken);

            return Accepted();
        }


        [HttpGet]
        public async Task<IActionResult> GetPayment([FromQuery] Guid orderId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetPaymentByOrderQuery { OrderId = orderId }, cancellationToken);
            return Ok(result);
        }
    }
}
