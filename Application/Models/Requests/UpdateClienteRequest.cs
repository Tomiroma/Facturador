using Application.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class UpdateClienteRequest : IRequest<ClienteResponse>
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "La Razón Social es oblgatoria.")]
        public string RazonSocial { get; set; } = string.Empty;
        [Required(ErrorMessage = "El CUIT es obligatorio.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El CUIT solo debe contener números.")]
        public string CUIT { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage = "Formato de mail inválido.")]
        public string? Email { get; set; }
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "El teléfono solo puede contener números.")]
        public string? Telefono { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Los días deben ser mayores a 0.")]
        public int DiasPlazoPago { get; set; }

        public int UsuarioId { get; set; }
    }
}
