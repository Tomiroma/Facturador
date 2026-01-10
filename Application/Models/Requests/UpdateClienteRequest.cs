using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class UpdateClienteRequest
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; } = string.Empty;
        public string CUIT { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public int DiasPlazoPago { get; set; }

    }
}
