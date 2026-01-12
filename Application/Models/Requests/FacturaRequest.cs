using Application.Models.Responses;
using MediatR;

public class FacturaRequest : IRequest<FacturaResponse>
{
    public string NumeroComprobante { get; set; } = string.Empty;
    public DateTime FechaEmision { get; set; }
    public decimal ImporteTotal { get; set; }
    public int ClienteId { get; set; }

    public DateTime? FechaVencimiento { get; set; }
    public string? Observaciones { get; set; }
}