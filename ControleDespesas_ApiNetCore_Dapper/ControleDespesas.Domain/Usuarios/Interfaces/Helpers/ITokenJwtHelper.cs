using ControleDespesas.Domain.Usuarios.Query.Results;

namespace ControleDespesas.Domain.Usuarios.Interfaces.Helpers
{
    public interface ITokenJwtHelper
    {
        string GenerarTokenJwt(UsuarioQueryResult usuarioQR);
    }
}