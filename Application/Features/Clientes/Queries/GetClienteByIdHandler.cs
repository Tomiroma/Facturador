using Application.Models.Responses;
using Application.Models.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;

namespace Application.Features.Clientes.Queries
{
    public class GetClienteByIdHandler : IRequestHandler<GetClienteByIdRequest, ClienteResponse>
    {
        private readonly IClienteRepository _repository;

        public GetClienteByIdHandler(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<ClienteResponse> Handle(GetClienteByIdRequest request, CancellationToken ct)
        {
            var cliente = await _repository.GetByIdAsync(request.Id);

            if (cliente == null)
            {
                throw new Exception($"No se encontró el cliente con ID {request.Id}.");
            }

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
