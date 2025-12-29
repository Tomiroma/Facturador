using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public string NombreCompleto {  get; set; } = string.Empty;

        public ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
    }
}
