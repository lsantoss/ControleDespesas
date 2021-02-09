using ControleDespesas.Domain.Commands.Usuario.Input;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Domain.Interfaces.Handlers
{
    public interface IUsuarioHandler
    {
        ICommandResult<Notificacao> Handler(AdicionarUsuarioCommand command);
        ICommandResult<Notificacao> Handler(AtualizarUsuarioCommand command);
        ICommandResult<Notificacao> Handler(ApagarUsuarioCommand command);
        ICommandResult<Notificacao> Handler(LoginUsuarioCommand command);
    }
}