using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Clientes.Queries
{
    public class GetAllClientesHandler: IRequestHandler<GetAllClientesRequest, List<ClienteResponse>>
    {
        private readonly IClienteRepository _repository;

        public GetAllClientesHandler(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ClienteResponse>> Handle(GetAllClientesRequest request, CancellationToken ct)
        {
            var clientes = await _repository.GetAllAsync();

            return clientes.Select(c => new ClienteResponse
            {
                Id = c.Id,
                RazonSocial = c.RazonSocial,
                CUIT = c.CUIT,
                Email = c.Email,
                Telefono = c.Telefono,
                DiasPlazoPago = c.DiasPlazoPago
            }).ToList();
        }
    }
}
