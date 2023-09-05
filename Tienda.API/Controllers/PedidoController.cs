using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Tienda.Application.Commands;
using Tienda.Application.Common.Enum;
using Tienda.Application.Queries;
using Tienda.Application.Response;

namespace Tienda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : BaseController
    {
        public PedidoController(IMediator _mediator) : base(_mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PedidoDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _mediator.Send(new PedidoQueryRequest(QueryType.Todos, -1));

            if (response == null)
                return NoContent();
            else if (response.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(List<PedidoDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromRoute][Required] int id)
        {
            var response = await _mediator.Send(new PedidoQueryRequest(QueryType.Uno, id));

            if (response == null || response.Count == 0)
                return NotFound();
            else
                return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PedidoDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAsync([FromBody][Required] CreatePedidoCommandRequest command)
        {
            var response = await _mediator.Send(command);
            return Created($"{Request.Path}/{response.Id}", response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(PedidoDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromBody][Required] UpdatePedidoCommandRequest command)
        {
            var response = await _mediator.Send(command);

            if (response == null)
                return NotFound("Id no encontrado.");
            else
                return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute][Required] int id)
        {
            var response = await _mediator.Send(new DeletePedidoCommandRequest(id));

            if (response == null)
                return NoContent();
            else if (response == true)
            {
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
