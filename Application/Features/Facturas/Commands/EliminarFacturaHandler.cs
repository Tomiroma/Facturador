using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Facturas.Commands
{
    public class EliminarFacturaHandler : IRequestHandler<DeleteFacturaRequest, GenericResponse>
    {
        private readonly IFacturaRepository _repository;

        public EliminarFacturaHandler(IFacturaRepository repository)
        {
            _repository = repository;
        }

        public async Task<GenericResponse> Handle(DeleteFacturaRequest request, CancellationToken ct)
        {
            var existente = await _repository.GetByIdAsync(request.Id);
            if (existente == null) throw new Exception("La factura no existe.");

            if (existente.FechaDePago.HasValue)
            {
                throw new Exception("No se puede eliminar una factura que ya figura como pagada. Primero anulá el pago si fue un error.");
            }

            await _repository.DeleteAsync(request.Id);
            await _repository.SaveChangesAsync();

            return new GenericResponse
            {
                Success = true,
                Message = "Factura eliminada con éxito"
            };
        }
    }
}
