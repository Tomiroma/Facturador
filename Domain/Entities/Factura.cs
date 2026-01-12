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

        public DateTime FechaLimiteCobro => Cliente != null
            ? FechaEmision.AddDays((double)(Cliente.DiasPlazoPago ?? 0))
            : FechaEmision;

        public decimal CalcularIva() => ImporteTotal - (ImporteTotal / 1.21m);

        public decimal CalcularNeto() => ImporteTotal / 1.21m;

        public string Estado
        {
            get
            {
                if (FechaDePago.HasValue) return "Pagada";

                if (DateTime.Now > FechaLimiteCobro) return "Reclamar Pago";

                return "Pendiente";
            }
        }
    }
}