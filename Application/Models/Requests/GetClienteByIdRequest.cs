using MediatR;
using Application.Models.Responses;

namespace Application.Models.Requests
{
    public class GetClienteByIdRequest : IRequest<ClienteResponse>
    {
        public int Id { get; set; }

        public GetClienteByIdRequest(int id)
        {
            Id = id;
        }
    }
}