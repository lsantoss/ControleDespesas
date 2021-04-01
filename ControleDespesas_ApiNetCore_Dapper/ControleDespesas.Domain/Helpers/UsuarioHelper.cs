using ControleDespesas.Domain.Commands.Usuario.Input;
using ControleDespesas.Domain.Commands.Usuario.Output;
using ControleDespesas.Domain.Entities;
using ControleDespesas.Infra.Commands;

namespace ControleDespesas.Domain.Helpers
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