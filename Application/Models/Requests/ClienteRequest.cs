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
    public class ClienteRequest : IRequest<ClienteResponse>
    {
        [Required(ErrorMessage = "La Razón Social es obligatoria.")]
        public string RazonSocial { get; set; } = string.Empty;

        [Required(ErrorMessage = "El CUIT es obligatorio")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El CUIT solo debe contener números.")]
        public string CUIT { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string? Email { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessage = "El telefono solo puede contener números.")]
        public string? Telefono { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Los días de plazo de pago deben ser mayorres a 0.")]
        public int? DiasPlazoPago { get; set; }

        [Required(ErrorMessage = "El ID de usuario es obligatorio.")]
        public int UsuarioId { get; set; }

    }
}
