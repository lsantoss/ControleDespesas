using ControleDespesas.Domain.Pessoas.Commands.Input;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Domain.Pessoas.Interfaces.Handlers
{
    public interface IPessoaHandler
    {
        ICommandResult<Notificacao> Handler(AdicionarPessoaCommand command);
        ICommandResult<Notificacao> Handler(int id, AtualizarPessoaCommand command);
        ICommandResult<Notificacao> Handler(int id);
    }
}