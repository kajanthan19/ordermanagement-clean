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
    public class ProductItemController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductItemController> _logger;
        public ProductItemController(ILogger<ProductItemController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductItemDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseCommandResponse))]
        public async Task<IActionResult> Get()
        {
            try
            {
                var query = new GetAllProductItemsQuery();
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
        [ProducesResponseType(typeof(ProductItemDto), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseCommandResponse))]
        public async Task<IActionResult> Post([FromBody] CreateProductItemDto model)
        {
            try
            {
                var command = new CreateProductItemCommand(model);
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
        [ProducesResponseType(typeof(CreateProductItemDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseCommandResponse))]
        public async Task<IActionResult> Update(int id, [FromBody] CreateProductItemDto model)
        {
            try
            {
                var command = new UpdateProductItemCommand(id, model);
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
                var command = new DeleteProductItemCommand(id);
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
