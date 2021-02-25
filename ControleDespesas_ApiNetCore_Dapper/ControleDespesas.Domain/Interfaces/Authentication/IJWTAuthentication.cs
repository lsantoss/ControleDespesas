using ControleDespesas.Domain.Query.Usuario.Results;

namespace ControleDespesas.Domain.Interfaces.Authentication
{
    public interface IJWTAuthentication
    {
        string GenerarTokenJwt(UsuarioQueryResult usuarioQR);
    }
}