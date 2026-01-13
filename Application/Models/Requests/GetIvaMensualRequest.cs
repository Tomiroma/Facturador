using Application.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class GetIvaMensualRequest : IRequest<IvaMensualResponse>
    {
        public int Mes { get; set; }
        public int Anio { get; set; }

        public GetIvaMensualRequest(int mes, int anio)
        {
            Mes = mes;
            Anio = anio;
        }
    }
}
