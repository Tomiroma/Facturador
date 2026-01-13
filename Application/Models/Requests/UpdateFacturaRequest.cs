using Application.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class UpdateFacturaRequest : IRequest<FacturaResponse>
    {
        public int Id { get; set; } 
        public string NumeroComprobante { get; set; } = string.Empty;
        public DateTime FechaEmision { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public DateTime? FechaDePago { get; set; }
        public decimal ImporteTotal { get; set; }
        public string? Observaciones { get; set; }
        public int ClienteId { get; set; }
    }
}
