using ControleDespesas.Domain.Commands.Pessoa.Input;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Domain.Interfaces.Handlers
{
    public interface IPessoaHandler
    {
        ICommandResult<Notificacao> Handler(AdicionarPessoaCommand command);
        ICommandResult<Notificacao> Handler(AtualizarPessoaCommand command);
        ICommandResult<Notificacao> Handler(ApagarPessoaCommand command);
    }
}