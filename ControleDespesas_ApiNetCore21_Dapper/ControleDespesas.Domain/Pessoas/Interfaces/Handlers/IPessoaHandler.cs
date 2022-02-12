using ControleDespesas.Domain.Pessoas.Commands.Input;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Domain.Pessoas.Interfaces.Handlers
{
    public interface IPessoaHandler
    {
        ICommandResult<Notificacao> Handler(long idUsuario, AdicionarPessoaCommand command);
        ICommandResult<Notificacao> Handler(long id, long idUsuario, AtualizarPessoaCommand command);
        ICommandResult<Notificacao> Handler(long id, long idUsuario);
    }
}