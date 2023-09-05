using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tienda.API.Middlewares;
using Tienda.Application.Commands;

namespace Tienda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(IMediator _mediator) : base(_mediator)
        {
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationCommandRequest command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
