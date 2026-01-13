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
    public class GetIvaMensualHandler : IRequestHandler<GetIvaMensualRequest, IvaMensualResponse>
    {
        private readonly IFacturaRepository _repository;

        public GetIvaMensualHandler(IFacturaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IvaMensualResponse> Handle(GetIvaMensualRequest request, CancellationToken ct)
        {
            var facturas = await _repository.GetAllAsync();

            var facturasDelMes = facturas
            .Where(f => f.FechaEmision.Month == request.Mes && f.FechaEmision.Year == request.Anio)
            .ToList();

            return new IvaMensualResponse
            {
                Mes = request.Mes,
                Anio = request.Anio,
                CantidadFacturas = facturasDelMes.Count,
                TotalNeto = facturasDelMes.Sum(f => f.CalcularNeto()),
                TotalIva = facturasDelMes.Sum(f => f.CalcularIva()),
                TotalFacturado = facturasDelMes.Sum(f => f.ImporteTotal)
            };
        }
    }
}
