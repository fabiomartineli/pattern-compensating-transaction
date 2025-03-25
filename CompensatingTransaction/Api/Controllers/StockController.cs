using Domain.Queries.Stocks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetPayment([FromQuery] Guid orderId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetStockByOrderQuery { OrderId = orderId }, cancellationToken);
            return Ok(result);
        }
    }
}
