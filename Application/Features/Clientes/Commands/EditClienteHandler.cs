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
    public class EditClienteHandler : IRequestHandler<UpdateClienteRequest, ClienteResponse>
    {
        private readonly IClienteRepository _repository;
        private readonly IUserRepository _userRepository;

        public EditClienteHandler(IClienteRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<ClienteResponse> Handle(UpdateClienteRequest request, CancellationToken ct)
        {
            var cliente = await _repository.GetByIdAsync(request.Id);
            if (cliente == null) throw new Exception("Cliente no encontrado.");

            var clienteConMismoCuit = await _repository.GetByCUITAsync(request.CUIT);
            if(clienteConMismoCuit != null && clienteConMismoCuit.Id != request.Id)
            {
                throw new Exception("El CUIT ingresado ya pertenece a otro cliente.");
            }

            var usuario = await _userRepository.GetByIdAsync(request.UsuarioId);
            if (usuario == null) throw new Exception("Usuario no válido.");

            cliente.RazonSocial = request.RazonSocial;
            cliente.CUIT = request.CUIT;
            cliente.Email = request.Email;
            cliente.Telefono = request.Telefono;
            cliente.DiasPlazoPago = request.DiasPlazoPago;
            cliente.UsuarioId = request.UsuarioId;

            await _repository.UpdateAsync(cliente);
            await _repository.SaveChangesAsync();

            return new ClienteResponse
            {
                Id = cliente.Id,
                RazonSocial = cliente.RazonSocial,
                CUIT = cliente.CUIT,
                Email = cliente.Email,
                Telefono = cliente.Telefono,
                DiasPlazoPago = cliente.DiasPlazoPago
            };
        }
    }
}
