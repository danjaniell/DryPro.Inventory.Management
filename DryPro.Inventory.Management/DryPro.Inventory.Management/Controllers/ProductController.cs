using DryPro.Inventory.Management.Application.Commands;
using DryPro.Inventory.Management.Application.Queries;
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

        [HttpPost("AddNew")]
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

        [HttpGet("GetById/{{id}}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Core.Entities.Product> GetById([FromQuery] int id) => await _mediator.Send(new GetByIdQuery() { Id = id });
    }
}