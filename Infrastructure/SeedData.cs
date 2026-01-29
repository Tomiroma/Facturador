using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence
{
    public static class DbInitializer
    {
        public static async Task SeedUsuarios(UserManager<Usuario> userManager)
        {
            if (!userManager.Users.Any())
            {
                var admin = new Usuario
                {
                    UserName = "Diego",
                    Email = "brokerage_dfr@yahoo.com.ar",
                    NombreCompleto = "Diego Romagnoli",
                    EmailConfirmed = true 
                };

                var result = await userManager.CreateAsync(admin, "ProyectoFactura2026!");

                if (!result.Succeeded)
                {
                    throw new Exception("Error al crear el usuario inicial: " +
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}