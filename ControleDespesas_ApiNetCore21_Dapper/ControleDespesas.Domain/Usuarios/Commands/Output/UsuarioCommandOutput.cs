using ControleDespesas.Domain.Usuarios.Enums;

namespace ControleDespesas.Domain.Usuarios.Commands.Output
{
    public class UsuarioCommandOutput
    {
        public long Id { get; private set; }
        public string Login { get; private set; }
        public string Senha { get; private set; }
        public EPrivilegioUsuario Privilegio { get; private set; }

        public UsuarioCommandOutput(long id, string login, string senha, EPrivilegioUsuario privilegio)
        {
            Id = id;
            Login = login;
            Senha = senha;
            Privilegio = privilegio;
        }
    }
}