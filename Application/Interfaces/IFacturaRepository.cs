using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFacturaRepository
    {
        Task<Factura?> GetByIdAsync(int id);

        Task<IEnumerable<Factura>> GetByClienteIdAsync(int clienteId);

        Task<IEnumerable<Factura>> GetByPeriodo(DateTime inicio, DateTime fin);

        Task<IEnumerable<Factura>> GetAllAsync();
        Task AddAsync(Factura factura);
        Task UpdateAsync(Factura factura);
        Task SaveChangesAsync();

    }
}
