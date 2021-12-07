using DryPro.Inventory.Management.Application.Commands;
using DryPro.Inventory.Management.Application.Queries.Product;
using DryPro.Inventory.Management.Application.Responses;
using DryPro.Inventory.Management.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Core.Entities.Product>> GetAllProducts() => await _mediator.Send(new GetAllProductsQuery());

        [HttpGet("GetByType/{{type}}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Core.Entities.Product>> GetByType([FromQuery] ProductType type) => await _mediator.Send(new GetByTypeQuery() { Type = type });

        [HttpGet("Get/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<Core.Entities.Product> GetById([FromRoute] string id) => (Core.Entities.Product)(await _mediator.Send(new GetByIdQuery() { Id = id }));

        [HttpPost("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<string>> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            var result = await _mediator.Send(command);
            if (result is null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpPost("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<string>> DeleteProduct([FromRoute] string id)
        {
            var result = await _mediator.Send(new DeleteProductCommand() { Id = id });
            if (result is null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpPost("DeleteAllAndGenerateRandomData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAllAndGenerateRandomData()
        {
            var result = await _mediator.Send(new DeleteAllAndGenerateRandomDataCommand());
            return Ok(result);
        }
    }
}