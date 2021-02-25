using ControleDespesas.Domain.Query.Usuario.Results;

namespace ControleDespesas.Domain.Interfaces.Services
{
    public interface ITokenJWTService
    {
        string GenerarTokenJwt(UsuarioQueryResult usuarioQR);
    }
}