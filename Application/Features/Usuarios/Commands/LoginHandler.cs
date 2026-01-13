using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Usuarios.Commands
{
    public class LoginHandler
    {
    }
    public async Task<AuthResponse> Handle(LoginRequest request, CancellationToken ct)
        {
            // 1. Buscar el usuario
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) throw new Exception("Credenciales incorrectas.");

            // 2. Verificar la contraseña (Identity hace el hashing automático)
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded)
            {
                // 3. Generar el Token JWT
                var token = GenerarJwtToken(user);
                return new AuthResponse { Token = token, Success = true };
            }

            throw new Exception("Credenciales incorrectas.");
        }
    }
