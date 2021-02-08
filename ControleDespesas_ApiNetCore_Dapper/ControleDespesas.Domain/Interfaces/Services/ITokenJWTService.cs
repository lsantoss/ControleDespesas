using ControleDespesas.Domain.Query.Usuario;

namespace ControleDespesas.Domain.Interfaces.Services
{
    public interface ITokenJWTService
    {
        string GenerateToken(UsuarioQueryResult usuarioQR);
    }
}