using Application.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class GetAllFacturasRequest : IRequest<List<FacturaResponse>>
    {

    }
}
