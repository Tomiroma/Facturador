using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Clientes.Commands
{
    public class CrearClienteHandler : IRequestHandler<ClienteRequest, ClienteResponse>
    {
        private readonly IClienteRepository _repository;

        public CrearClienteHandler(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<ClienteResponse> Handle(ClienteRequest request, CancellationToken ct)
        {
            var existente = await _repository.GetByCUITAsync(request.CUIT);

            if (existente != null)
            {
                throw new Exception("Ya existe un cliente con ese CUIT.");
            }




            var nuevoCliente = new Domain.Entities.Cliente
            {
                RazonSocial = request.RazonSocial,
                CUIT = request.CUIT,
                Email = request.Email,
                Telefono = request.Telefono,
                DiasPlazoPago = request.DiasPlazoPago,
                UsuarioId = "1"

            };

            await _repository.AddAsync(nuevoCliente);
            await _repository.SaveChangesAsync();

            return new ClienteResponse
            {
                Id = nuevoCliente.Id,
                CUIT = nuevoCliente.CUIT,
                RazonSocial = nuevoCliente.RazonSocial,
                Email = nuevoCliente.Email,
                Telefono = nuevoCliente.Telefono,
                DiasPlazoPago = nuevoCliente.DiasPlazoPago

            };
        }
    }
}
