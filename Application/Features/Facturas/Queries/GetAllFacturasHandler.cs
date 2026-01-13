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
    public class GetAllFacturasHandler: IRequestHandler <GetAllFacturasRequest, List<FacturaResponse>>
    {
        private readonly IFacturaRepository _repository;

        public GetAllFacturasHandler(IFacturaRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<FacturaResponse>> Handle(GetAllFacturasRequest request, CancellationToken ct)
        {
            var facturas = await _repository.GetAllAsync();

            return facturas
                .OrderByDescending(f => f.FechaEmision)
                .Select(f => new FacturaResponse
                {
                    Id = f.Id,
                    NumeroComprobante = f.NumeroComprobante,
                    FechaEmision = f.FechaEmision,
                    FechaVencimiento = f.FechaVencimiento,
                    FechaDePago = f.FechaDePago,
                    ImporteTotal = f.ImporteTotal,
                    ImporteNeto = f.CalcularNeto(),
                    ImporteIva = f.CalcularIva(),
                    Estado = f.Estado,
                    ClienteId = f.ClienteId,
                    ClienteRazonSocial = f.Cliente?.RazonSocial ?? "Sin Cliente",
                    Observaciones = f.Observaciones
                }).ToList();
        }
    }
}
