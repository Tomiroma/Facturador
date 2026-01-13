using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Responses
{
    public class IvaMensualResponse
    {
        public int Mes { get; set; }
        public int Anio { get; set; }
        public decimal TotalNeto { get; set; }
        public decimal TotalIva { get; set; }
        public decimal TotalFacturado { get; set; }
        public int CantidadFacturas { get; set; }
    }
}
