using ControleDespesas.Domain.Usuarios.Commands.Input;
using ControleDespesas.Domain.Usuarios.Commands.Output;
using ControleDespesas.Domain.Usuarios.Entities;
using ControleDespesas.Infra.Commands;

namespace ControleDespesas.Domain.Usuarios.Helpers
{
    public static class UsuarioHelper
    {
        public static Usuario GerarEntidade(AdicionarUsuarioCommand command)
        {
            Usuario usuario = new Usuario(0, command.Login, command.Senha, command.Privilegio);
            usuario.Validar();
            return usuario;
        }

        public static Usuario GerarEntidade(AtualizarUsuarioCommand command)
        {
            Usuario usuario = new Usuario(command.Id, command.Login, command.Senha, command.Privilegio);
            usuario.Validar();
            return usuario;
        }

        public static UsuarioCommandOutput GerarDadosRetornoInsert(Usuario usuario)
        {
            return new UsuarioCommandOutput
            {
                Id = usuario.Id,
                Login = usuario.Login,
                Senha = usuario.Senha,
                Privilegio = usuario.Privilegio
            };
        }

        public static UsuarioCommandOutput GerarDadosRetornoUpdate(Usuario usuario)
        {
            return new UsuarioCommandOutput
            {
                Id = usuario.Id,
                Login = usuario.Login,
                Senha = usuario.Senha,
                Privilegio = usuario.Privilegio
            };
        }

        public static CommandOutput GerarDadosRetornoDelete(int id)
        {
            return new CommandOutput { Id = id };
        }
    }
}