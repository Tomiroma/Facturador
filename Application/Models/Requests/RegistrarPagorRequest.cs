using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class RegistrarPagorRequest
    {
        public int FacturaId { get; set; }
        public DateTime FechaDePago { get; set; } = DateTime.Now;
    }
}
