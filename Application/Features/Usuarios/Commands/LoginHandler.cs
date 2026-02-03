using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt; 
using System.Security.Claims;
using System.Text;

namespace Application.Features.Usuarios.Commands
{
    public class LoginHandler : IRequestHandler<LoginRequest, AuthResponse>
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IConfiguration _configuration;

        public LoginHandler(UserManager<Usuario> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AuthResponse> Handle(LoginRequest request, CancellationToken ct)
        {
            var usuario = await _userManager.FindByEmailAsync(request.Email);

            if (usuario == null) return new AuthResponse { Success = false, Message = "Usuario no encontrado" };

            var resultado = await _userManager.CheckPasswordAsync(usuario, request.Password);

            if (!resultado) return new AuthResponse { Success = false, Message = "Contraseña Incorrecta" };

            var token = GenerarTokenJwt(usuario);

            return new AuthResponse
            {
                Success = true,
                Token = token,
                NombreCompleto = usuario.NombreCompleto,
                Message = "Bienvenido de nuevo Dieguito"
            };


        }
        private string GenerarTokenJwt(Usuario usuario)
        {
            var claims = new List<Claim> {
        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
        new Claim(ClaimTypes.Email, usuario.Email!),
        new Claim("NombreCompleto", usuario.NombreCompleto),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // ID único del token
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
