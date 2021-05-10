using ControleDespesas.Domain.Usuarios.Enums;

namespace ControleDespesas.Domain.Usuarios.Query.Results
{
    public class UsuarioTokenQueryResult
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public EPrivilegioUsuario Privilegio { get; set; }
        public string Token { get; set; }
    }
}