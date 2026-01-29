using Application.Models.Requests;
using Application.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Facturador.Controllers
{
    [Authorize]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(new DeleteClienteRequest { Id = id });
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new GenericResponse
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteResponse>> GetById(int id, CancellationToken ct)
        {
            try
            {
                var response = await _mediator.Send(new GetClienteByIdRequest(id));
                return Ok(response);
            }

            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<ClienteResponse>>> GetAll()
        {
            try
            {
                var response = await _mediator.Send(new GetAllClientesRequest());
                return Ok(response);
            }
            catch (Exception ex)
            {
                
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
