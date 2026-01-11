using Application.Models.Requests;
using Application.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Facturador.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ClientesController(IMediator mediator)
                {
                    _mediator = mediator;
                }

        [HttpPost]
        public async Task<ActionResult<ClienteResponse>> Create([FromBody] ClienteRequest request)
        {
            try
            {
                var response = await _mediator.Send(request);

                return Ok(response);
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClienteResponse>> Update(int id, [FromBody] UpdateClienteRequest request)
        {
            if (id != request.Id) return BadRequest("El ID del cliente no coincide.");

            try
            {
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
