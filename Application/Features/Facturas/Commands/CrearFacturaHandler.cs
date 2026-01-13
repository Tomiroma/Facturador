using Application.Interfaces;
using Application.Models.Responses;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Facturas.Commands
{
    public class CrearFacturaHandler : IRequestHandler<FacturaRequest, FacturaResponse>
    {
        private readonly IFacturaRepository _repository;
        private readonly IClienteRepository _clienteRepository;

        public CrearFacturaHandler(IFacturaRepository repository, IClienteRepository clienteRepository)
        {
            _repository = repository;
            _clienteRepository = clienteRepository;
        }
        public async Task<FacturaResponse> Handle(FacturaRequest request, CancellationToken ct)
        {
            var cliente = await _clienteRepository.GetByIdAsync(request.ClienteId);
            if (cliente == null) throw new Exception("Cliente no encontrado.");

            if (request.ImporteTotal <= 0)
                throw new Exception("El importe total de la factura debe ser mayor a cero.");

            var facturaExistente = await _repository.GetByNumComprobante(request.NumeroComprobante);
            if (facturaExistente != null)
                throw new Exception($"La factura {request.NumeroComprobante} ya fue cargada anteriormente.");

            if (request.FechaEmision == default)
            {
                request.FechaEmision = DateTime.Now; 
            }

            if (request.FechaVencimiento.HasValue && request.FechaVencimiento < request.FechaEmision)
                throw new Exception("La fecha de vencimiento legal no puede ser anterior a la fecha de emisión.");

            DateTime? fechaEsperada = null;

            if (cliente.DiasPlazoPago.HasValue && cliente.DiasPlazoPago.Value > 0)
            {
                fechaEsperada = request.FechaEmision.AddDays((double)cliente.DiasPlazoPago.Value);
            }

            var nuevaFactura = new Factura
            {
                NumeroComprobante = request.NumeroComprobante,
                FechaEmision = request.FechaEmision,
                FechaVencimiento = fechaEsperada,
                ImporteTotal = request.ImporteTotal,
                Observaciones = request.Observaciones,
                ClienteId = request.ClienteId,
                Cliente = cliente
            };

            await _repository.AddAsync(nuevaFactura);
            await _repository.SaveChangesAsync();

            return new FacturaResponse
            {
                Id = nuevaFactura.Id,
                NumeroComprobante = nuevaFactura.NumeroComprobante,
                FechaEmision = nuevaFactura.FechaEmision,
                FechaVencimiento = nuevaFactura.FechaVencimiento,
                FechaDePago = nuevaFactura.FechaDePago,

                ImporteTotal = nuevaFactura.ImporteTotal,
                ImporteNeto = nuevaFactura.CalcularNeto(),
                ImporteIva = nuevaFactura.CalcularIva(),

                Estado = nuevaFactura.Estado,

                ClienteId = nuevaFactura.ClienteId,
                ClienteRazonSocial = cliente.RazonSocial,
                Observaciones = nuevaFactura.Observaciones
            };
        }
    }
}
