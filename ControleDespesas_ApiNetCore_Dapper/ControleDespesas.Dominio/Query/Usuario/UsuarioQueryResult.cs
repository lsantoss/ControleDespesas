using ControleDespesas.Domain.Enums;

namespace ControleDespesas.Domain.Query.Usuario
{
    public class UsuarioQueryResult
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public EPrivilegioUsuario Privilegio { get; set; }
    }
}