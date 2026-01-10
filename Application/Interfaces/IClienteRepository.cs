using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente?> GetByIdAsync(int id);
        Task<IEnumerable<Cliente>> GetAllAsync();

        Task<Cliente?> GetByCUITAsync(string cuit);

        Task AddAsync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);

        Task SaveChangesAsync();
        Task DeleteAsync(int id);
    }
}
