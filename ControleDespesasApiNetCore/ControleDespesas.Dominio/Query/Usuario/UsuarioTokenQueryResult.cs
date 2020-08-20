using ControleDespesas.Dominio.Enums;

namespace ControleDespesas.Dominio.Query.Usuario
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