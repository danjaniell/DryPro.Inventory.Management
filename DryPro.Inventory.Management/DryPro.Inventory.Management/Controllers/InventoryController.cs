using DryPro.Inventory.Management.Application.Commands;
using DryPro.Inventory.Management.Application.Queries.Inventory;
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
    public class InventoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InventoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAvailableProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Core.Entities.Product>> GetAvailableProducts() => await _mediator.Send(new GetAllProductsQuery());

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Core.Entities.Inventory>> GetAll() => await _mediator.Send(new GetAllInventoryQuery());

        [HttpPost("SaveOne")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<string>> Save([FromBody] SaveInventoryCommand command)
        {
            var result = await _mediator.Send(command);
            if (result is null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpPost("SaveAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<string>> SaveInventory([FromBody] SaveAllInventoryCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == 0)
            {
                return NoContent();
            }
            return Ok(result);
        }
    }
}