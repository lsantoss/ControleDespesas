using ControleDespesas.Domain.Usuarios.Enums;

namespace ControleDespesas.Domain.Usuarios.Query.Results
{
    public class UsuarioQueryResult
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public EPrivilegioUsuario Privilegio { get; set; }
    }
}