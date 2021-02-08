using ControleDespesas.Api.Settings;
using ControleDespesas.Domain.Interfaces.Services;
using ControleDespesas.Domain.Query.Usuario;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ControleDespesas.Api.Services
{
    public class TokenJWTService : ITokenJWTService
    {
        private readonly string _ChaveJWT;

        public TokenJWTService(IOptions<SettingsAPI> options)
        {
            _ChaveJWT = options.Value.ChaveJWT;
        }

        public string GenerateToken(UsuarioQueryResult usuarioQR)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_ChaveJWT);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuarioQR.Login.ToString()),
                    new Claim(ClaimTypes.Role, usuarioQR.Privilegio.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}