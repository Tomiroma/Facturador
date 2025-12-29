using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Factura
    {
        public int Id { get; set; }
        public string NumeroComprobante { get; set; } = string.Empty;

        public DateTime FechaEmision { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public DateTime? FechaDePago { get; set; }
        public decimal ImporteTotal { get; set; }
        public string? Observaciones { get; set; }
        
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;

        public decimal CalcularIva() => ImporteTotal - (ImporteTotal / 1.21m);

        public decimal CalcularNeto() => ImporteTotal / 1.21m;

        public string Estado
        {
            get
            {
                if (FechaDePago.HasValue) return "Pagada";
                if (FechaVencimiento.HasValue && FechaVencimiento < DateTime.Now) return "Vencida";
                return "Pendiente";
            }
        }

    }
}
