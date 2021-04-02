using ControleDespesas.Domain.Query.Usuario.Results;

namespace ControleDespesas.Domain.Interfaces.Helpers
{
    public interface ITokenJwtHelper
    {
        string GenerarTokenJwt(UsuarioQueryResult usuarioQR);
    }
}