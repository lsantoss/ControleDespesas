using ControleDespesas.Domain.Enums;

namespace ControleDespesas.Domain.Commands.Usuario.Output
{
    public class UsuarioCommandOutput
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public EPrivilegioUsuario Privilegio { get; set; }
    }
}