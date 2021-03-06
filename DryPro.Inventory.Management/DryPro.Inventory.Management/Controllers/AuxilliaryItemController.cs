using DryPro.Inventory.Management.Application.Commands;
using DryPro.Inventory.Management.Application.Queries;
using DryPro.Inventory.Management.Application.Queries.AuxilliaryItem;
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
    public class AuxilliaryItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuxilliaryItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AuxilliaryItemResponse>> CreateAuxilliaryItem([FromBody] CreateAuxilliaryItemCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("Get/fromProduct/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<List<Core.Entities.AuxilliaryItem>> GetAllAuxItems([FromRoute] string productId) => await _mediator.Send(new GetAllAuxItemsQuery(productId));

        [HttpGet("Get/fromProduct/{productId}/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<Core.Entities.AuxilliaryItem> GetById([FromRoute] string productId, [FromRoute] int id) => await _mediator.Send(new GetByIdQuery(productId) {Id = id});

        [HttpPost("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<string>> UpdateAuxilliaryItem([FromBody] UpdateAuxilliaryItemCommand command)
        {
            var result = await _mediator.Send(command);
            if (result is null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpPost("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<string>> DeleteAuxilliaryItem([FromBody] DeleteAuxilliaryItemCommand command)
        {
            var result = await _mediator.Send(command);
            if (result is null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("GetTotalCost/fromProduct/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<decimal> GetTotalCost([FromRoute] string productId) => await _mediator.Send(new GetTotalCostQuery(productId));
    }
}