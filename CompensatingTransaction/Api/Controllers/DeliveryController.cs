using Application.Settings;
using Domain.Commands.Deliveries.Confirm;
using Domain.Commands.Deliveries.Fail;
using Domain.Queries.Deliveries;
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
    [Route("api/delivery")]
    public class DeliveryController : ControllerBase
    {
        private readonly IMessageBusPublisher _messageBusPublisher;
        private readonly IMediator _mediator;
        private readonly MessageBusDestinationSettings _messageBusDestination;

        public DeliveryController(IMessageBusPublisher messageBusPublisher,
            IOptions<MessageBusDestinationSettings> options,
            IMediator mediator)
        {
            _messageBusPublisher = messageBusPublisher;
            _messageBusDestination = options.Value;
            _mediator = mediator;
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> Confirm([FromBody] ConfirmDeliveryCommand request, CancellationToken cancellationToken)
        {
            await _messageBusPublisher.ExecuteAsync(new MessageBusQueuePublisherRequest<ConfirmDeliveryCommand>
            {
                Content = request,
                Destination = _messageBusDestination.QueueDeliveryConfirm,
                Id = Guid.CreateVersion7().ToString(),
            }, cancellationToken);

            return Accepted();
        }

        [HttpPost("fail")]
        public async Task<IActionResult> Fail([FromBody] FailDeliveryCommand request, CancellationToken cancellationToken)
        {
            await _messageBusPublisher.ExecuteAsync(new MessageBusQueuePublisherRequest<FailDeliveryCommand>
            {
                Content = request,
                Destination = _messageBusDestination.QueueDeliveryFail,
                Id = Guid.CreateVersion7().ToString(),
            }, cancellationToken);

            return Accepted();
        }


        [HttpGet]
        public async Task<IActionResult> GetDelviery([FromQuery] Guid orderId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetDeliveryByOrderQuery { OrderId = orderId }, cancellationToken);
            return Ok(result);
        }
    }
}
