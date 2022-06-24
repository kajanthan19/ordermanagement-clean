using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Core.Exceptions;
using OrderManagement.Core.Handlers.Commands;
using OrderManagement.Core.Handlers.Queries;
using OrderManagement.Core.Responses;
using OrderManagement.Domain.Dtos;
using System.Net;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrderController> _logger;
        public OrderController(ILogger<OrderController> logger,IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpGet("{email}")]
        [ProducesResponseType(typeof(IEnumerable<PersonDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseCommandResponse))]
        public async Task<IActionResult> Get(string email)
        {
            try
            {
                var query = new GetAllOrdersByPersonCommand(email);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Catch Exception error {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderDto), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseCommandResponse))]
        public async Task<IActionResult> Post([FromBody] CreateOrderDto model)
        {
            try
            {
                var command = new CreateOrderCommand(model);
                var response = await _mediator.Send(command);
                return StatusCode((int)HttpStatusCode.Created, response);
            }
            catch (InvalidRequestBodyException ex)
            {
                _logger.LogError($"Catch Exception error {ex.Message}");
                return BadRequest(new BaseCommandResponse
                {
                    Success = false,
                    Errors = ex.Errors.ToList()
                });
            }
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(CreateOrderDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseCommandResponse))]
        public async Task<IActionResult> Update(int id, [FromBody] CreateOrderDto model)
        {
            try
            {
                var command = new UpdateOrderCommand(id, model);
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (InvalidRequestBodyException ex)
            {
                _logger.LogError($"Catch Exception error {ex.Message}");
                return BadRequest(new BaseCommandResponse
                {
                    Success = false,
                    Errors = ex.Errors.ToList()
                });
            }
        }


        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseCommandResponse))]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var command = new DeleteOrderCommand(id);
                await _mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Catch Exception error {ex.Message}");
                return BadRequest(new BaseCommandResponse
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

    }
}
