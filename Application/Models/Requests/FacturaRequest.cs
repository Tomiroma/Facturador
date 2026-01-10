using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class FacturaRequest
    {
        public string NumeroComprobante { get; set; } = string.Empty;

        public DateTime FechaEmision { get; set; } = DateTime.Now;

        public decimal ImporteTotal { get; set; }

        public int ClienteId { get; set; }

        public string? Observaciones { get; set; }

        public DateTime? FechaVencimiento { get; set; }
        public DateTime? FechaPago  { get; set; }
    }
}
