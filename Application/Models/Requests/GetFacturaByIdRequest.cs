using Application.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class GetFacturaByIdRequest : IRequest<FacturaResponse>
    {
        public int Id { get; set; }

        public GetFacturaByIdRequest(int id)
        {
            Id = id;
        }
    }
}
