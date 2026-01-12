using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Responses
{
    public class FacturaResponse
    {
        public int Id { get; set; }
        public string NumeroComprobante { get; set; } = string.Empty;

        public DateTime FechaEmision { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public DateTime? FechaDePago { get; set; }

        public decimal ImporteTotal { get; set; }
        public decimal ImporteNeto { get; set; }
        public decimal ImporteIva { get; set; }

        public string Estado { get; set; } = string.Empty;
        public int ClienteId { get; set; }
        public string ClienteRazonSocial { get; set; } = string.Empty;
        public string? Observaciones { get; set; }

    }
}
