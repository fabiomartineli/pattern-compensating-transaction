using Application.Settings;
using Domain.Commands.Orders.Create;
using Domain.Queries.Orders;
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
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IMessageBusPublisher _messageBusPublisher;
        private readonly IMediator _mediator;
        private readonly MessageBusDestinationSettings _messageBusDestination;

        public OrderController(IMessageBusPublisher messageBusPublisher,
            IOptions<MessageBusDestinationSettings> options,
            IMediator mediator)
        {
            _messageBusPublisher = messageBusPublisher;
            _messageBusDestination = options.Value;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand request, CancellationToken cancellationToken)
        {
            await _messageBusPublisher.ExecuteAsync(new MessageBusQueuePublisherRequest<CreateOrderCommand>
            {
                Content = request,
                Destination = _messageBusDestination.QueueOrderCreate,
                Id = Guid.CreateVersion7().ToString(),
            }, cancellationToken);

            return Accepted(new { request.Id });
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder([FromRoute] Guid orderId, CancellationToken cancellationToken)
        {
           var result = await _mediator.Send(new GetOrderByIdQuery { Id = orderId }, cancellationToken);
            return Ok(result);
        }
    }
}
