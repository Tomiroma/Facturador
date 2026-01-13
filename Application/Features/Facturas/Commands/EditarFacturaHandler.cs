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
    public class EditarFacturaHandler : IRequestHandler<UpdateFacturaRequest, FacturaResponse>
    {
        private readonly IFacturaRepository _repository;
        private readonly IClienteRepository _clienteRepository;

        public EditarFacturaHandler(IFacturaRepository repository, IClienteRepository clienteRepository)
        {
            _repository = repository;
            _clienteRepository = clienteRepository;
        }

        public async Task<FacturaResponse> Handle(UpdateFacturaRequest request, CancellationToken ct)
        {
           
            var factura = await _repository.GetByIdAsync(request.Id);
            if (factura == null) throw new Exception("La factura no existe.");

          
            if (factura.FechaDePago.HasValue)
                throw new Exception("No se puede editar una factura que ya figura como pagada.");

           
            if (factura.ClienteId != request.ClienteId)
            {
                var nuevoCliente = await _clienteRepository.GetByIdAsync(request.ClienteId);
                if (nuevoCliente == null) throw new Exception("El nuevo cliente seleccionado no existe.");

                factura.Cliente = nuevoCliente;
                factura.ClienteId = request.ClienteId;
            }

            if (factura.Cliente != null && factura.Cliente.DiasPlazoPago.HasValue && factura.Cliente.DiasPlazoPago > 0)
            {
                factura.FechaVencimiento = request.FechaEmision.AddDays((double)factura.Cliente.DiasPlazoPago.Value);
            }
            else
            {
                factura.FechaVencimiento = null;
            }

            factura.NumeroComprobante = request.NumeroComprobante;
            factura.FechaEmision = request.FechaEmision;
            factura.FechaDePago = request.FechaDePago;
            factura.ImporteTotal = request.ImporteTotal;
            factura.Observaciones = request.Observaciones;

            await _repository.UpdateAsync(factura);
            await _repository.SaveChangesAsync();

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
                ClienteRazonSocial = factura.Cliente?.RazonSocial ?? "Sin Cliente"
            };
        }
    }
}
