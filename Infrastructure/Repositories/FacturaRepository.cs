using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly ApplicationDbContext _context;

        public FacturaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Factura factura)
        {
            await _context.Facturas.AddAsync(factura);
        }

        public async Task DeleteAsync(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
            }
        }

        public async Task<IEnumerable<Factura>> GetAllAsync()
        {
            return await _context.Facturas
                .Include(f => f.Cliente)
                .OrderByDescending(f => f.FechaEmision)
                .ToListAsync();
        }

        public async Task<IEnumerable<Factura>> GetByClienteIdAsync(int clienteId)
        {
            return await _context.Facturas
                .Where(f => f.ClienteId == clienteId)
                .OrderByDescending(f => f.FechaEmision)
                .ToListAsync();
        }

        public async Task<Factura?> GetByIdAsync(int id)
        {
            return await _context.Facturas
                .Include(f => f.Cliente)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Factura?> GetByNumComprobante(string numComprobante)
        {
            return await _context.Facturas
               .Include(f => f.Cliente)
               .FirstOrDefaultAsync(f => f.NumeroComprobante == numComprobante);
        }

        public async Task<IEnumerable<Factura>> GetByPeriodo(DateTime inicio, DateTime fin)
        {
            return await _context.Facturas
                .Where(f => f.FechaEmision >= inicio && f.FechaEmision <= fin)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Factura factura)
        {
            _context.Facturas.Update(factura);
        }
    }
}
