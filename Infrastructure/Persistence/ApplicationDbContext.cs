using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; 
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{

    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    
        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Factura> Facturas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.CUIT)
                .IsUnique();

            modelBuilder.Entity<Factura>()
                .Property(f => f.ImporteTotal)
                .HasPrecision(18, 2);
        }
    }
}