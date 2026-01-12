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
    public class DeleteClienteHandler : IRequestHandler<DeleteClienteRequest, GenericResponse>
    {
        private readonly IClienteRepository _repository;

        public DeleteClienteHandler(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<GenericResponse> Handle(DeleteClienteRequest request, CancellationToken ct)
        {
            var existente = await _repository.GetByIdAsync(request.Id);

            if (existente == null) throw new Exception("No se puede eliminar: Cliente no encontrado.");

            await _repository.DeleteAsync(request.Id);
            await _repository.SaveChangesAsync();

            return new GenericResponse
            {
                Success = true,
                Message = "Cliente eliminado con éxito"
            };
        }
    }
}
