using ControleDespesas.Domain.Pagamentos.Commands.Input;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Domain.Pagamentos.Interfaces.Handlers
{
    public interface IPagamentoHandler
    {
        ICommandResult<Notificacao> Handler(AdicionarPagamentoCommand command);
        ICommandResult<Notificacao> Handler(int id, AtualizarPagamentoCommand command);
        ICommandResult<Notificacao> Handler(int id);
    }
}