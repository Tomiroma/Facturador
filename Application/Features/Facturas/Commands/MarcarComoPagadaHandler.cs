using Application.Exceptions;
using Application.Interfaces;
using Application.Models.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Facturas.Commands
{
    public class MarcarComoPagadaHandler : IRequestHandler<RegistrarPagoRequest, bool>
    {
        private readonly IFacturaRepository _repository;

        public MarcarComoPagadaHandler(IFacturaRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(RegistrarPagoRequest request, CancellationToken ct)
        {
            var factura = await _repository.GetByIdAsync(request.FacturaId);

            if (factura == null) return false;

            if (factura.FechaDePago != null)
            {
                throw new BusinessException("La Factura ya fue pagada");
            }

            factura.RegistrarPago(request.FechaDePago);

            await _repository.UpdateAsync(factura);
            return true;
        }
    }
}
