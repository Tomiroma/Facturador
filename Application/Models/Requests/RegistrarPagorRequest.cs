using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class RegistrarPagoRequest : IRequest<bool>
    {
        public int FacturaId { get; set; }
        public DateTime FechaDePago { get; set; } = DateTime.Now;
    }
}
