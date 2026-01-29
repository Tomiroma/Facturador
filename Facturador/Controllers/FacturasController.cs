using Application.Models.Requests;
using Application.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Facturador.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FacturasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FacturasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<FacturaResponse>> Create([FromBody] FacturaRequest request)
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
        public async Task<ActionResult<FacturaResponse>> Update(int id, [FromBody] UpdateFacturaRequest command)
        {
            if (id != command.Id) return BadRequest(new { message = "El ID no coincide" });

            try
            {
                var response = await _mediator.Send(command);
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
                var response = await _mediator.Send(new DeleteFacturaRequest { Id = id });
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
        public async Task<ActionResult<FacturaResponse>> GetById(int id)
        {
            try
            {
                var response = await _mediator.Send(new GetFacturaByIdRequest(id));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<FacturaResponse>>> GetAll()
        {
            try
            {
                var response = await _mediator.Send(new GetAllFacturasRequest());
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("reporte-iva/{anio}/{mes}")]
        public async Task<ActionResult<IvaMensualResponse>> GetIvaReport(int anio, int mes)
        {
            try
            {
                var response = await _mediator.Send(new GetIvaMensualRequest(mes, anio));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}