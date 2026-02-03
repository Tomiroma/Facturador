using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; } = string.Empty;
        public string CUIT { get; set; } = string.Empty;
        public string? Email { get; set; }

        public string? Telefono { get; set; }
        public int? DiasPlazoPago { get; set; }

        [ForeignKey("UsuarioId")]
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; } = null!; //Operador de perdón de nulos, parece nulo ahora pero cuando alguien lo use va a tener un valor

        public ICollection<Factura> Facturas { get; set; } = new List<Factura>();


    }
}
