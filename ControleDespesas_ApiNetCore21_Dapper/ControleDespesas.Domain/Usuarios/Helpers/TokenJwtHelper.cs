using ControleDespesas.Domain.Usuarios.Interfaces.Helpers;
using ControleDespesas.Domain.Usuarios.Query.Results;
using ControleDespesas.Infra.Settings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ControleDespesas.Domain.Usuarios.Helpers
{
    public class TokenJwtHelper : ITokenJwtHelper
    {
        private readonly SettingsApi _settings;

        public TokenJwtHelper(SettingsApi settings)
        {
            _settings = settings;
        }

        public string GenerarTokenJwt(UsuarioQueryResult usuarioQR)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.ChaveJWT);
            var subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, usuarioQR.Login.ToString()),
                new Claim(ClaimTypes.Role, usuarioQR.Privilegio.ToString())
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}