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
            return new Usuario(command.Login, command.Senha, command.Privilegio);
        }

        public static Usuario GerarEntidade(AtualizarUsuarioCommand command)
        {
            return new Usuario(command.Id, command.Login, command.Senha, command.Privilegio);
        }

        public static UsuarioCommandOutput GerarDadosRetorno(Usuario usuario)
        {
            return new UsuarioCommandOutput(usuario.Id, usuario.Login, usuario.Senha, usuario.Privilegio);
        }

        public static CommandOutput GerarDadosRetornoDelete(long id)
        {
            return new CommandOutput(id);
        }
    }
}