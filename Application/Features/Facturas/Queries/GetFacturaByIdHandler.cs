using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Facturas.Queries
{
    public class GetFacturaByIdHandler : IRequestHandler<GetFacturaByIdRequest, FacturaResponse>
    {
        private readonly IFacturaRepository _repository;

        public GetFacturaByIdHandler(IFacturaRepository repository)
        {
            _repository = repository;
        }

        public async Task<FacturaResponse> Handle(GetFacturaByIdRequest request, CancellationToken ct)
        {
            var factura = await _repository.GetByIdAsync(request.Id);

            if (factura == null) throw new Exception("La factura solicitada no existe.");

            return new FacturaResponse
            {
                Id = factura.Id,
                NumeroComprobante = factura.NumeroComprobante,
                FechaEmision = factura.FechaEmision,
                FechaVencimiento = factura.FechaVencimiento,
                FechaDePago = factura.FechaDePago,
                ImporteTotal = factura.ImporteTotal,
                ImporteNeto = factura.CalcularNeto(),
                ImporteIva = factura.CalcularIva(),
                Estado = factura.Estado,
                ClienteId = factura.ClienteId,
                ClienteRazonSocial = factura.Cliente?.RazonSocial ?? "Cliente no cargado",
                Observaciones = factura.Observaciones
            };
        }
    }
}
