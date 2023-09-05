using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Tienda.Application.Commands;
using Tienda.Application.Queries;
using Tienda.Application.Response;

namespace Tienda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : BaseController
    {
        public ProductoController(IMediator _mediator) : base(_mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ProductoDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _mediator.Send(new ProductoQueryRequest());

            if (response == null)
                return NoContent();
            else if (response.Count == 0)
            {
                return NoContent();
            } else
            {
                return Ok(response);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(List<ProductoDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetByIdAsync([FromRoute][Required] int id)
        {
            var response = await _mediator.Send(new ProductoQueryRequest(id));

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

        [HttpPost]
        [ProducesResponseType(typeof(ProductoDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAsync([FromBody][Required] CreateProductoCommandRequest command)
        {
            var response = await _mediator.Send(command);
            return Created($"{Request.Path}/{response.Id}", response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ProductoDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromBody][Required] UpdateProductoCommandRequest command)
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
            var response = await _mediator.Send(new DeleteProductoCommandRequest(id));

            if (response == false)
                return NotFound();
            else 
            {
                return NoContent();
            }
        }
    }
}
