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
        private readonly IUserRepository _userRepository;

        public CrearClienteHandler(IClienteRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<ClienteResponse> Handle(ClienteRequest request, CancellationToken ct)
        {
            var existente = await _repository.GetByCUITAsync(request.CUIT);

            if (existente != null)
            {
                throw new Exception("Ya existe un cliente con ese CUIT.");
            }

            var usuario = _userRepository.GetByIdAsync(request.UsuarioId);
            if (usuario == null)
            {
                throw new Exception($"Error: No se encontró un usuario con el ID {request.UsuarioId}. El cliente debe estar vinculado a un usuario válido.");
            }




            var nuevoCliente = new Domain.Entities.Cliente
            {
                RazonSocial = request.RazonSocial,
                CUIT = request.CUIT,
                Email = request.Email,
                Telefono = request.Telefono,
                DiasPlazoPago = request.DiasPlazoPago,
                UsuarioId = request.UsuarioId,

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
