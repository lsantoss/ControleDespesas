using ControleDespesas.Domain.Commands.Pagamento.Input;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Domain.Interfaces.Handlers
{
    public interface IPagamentoHandler
    {
        ICommandResult<Notificacao> Handler(AdicionarPagamentoCommand command);
        ICommandResult<Notificacao> Handler(AtualizarPagamentoCommand command);
        ICommandResult<Notificacao> Handler(int id);
    }
}