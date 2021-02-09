using ControleDespesas.Domain.Commands.TipoPagamento.Input;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Domain.Interfaces.Handlers
{
    public interface ITipoPagamentoHandler
    {
        ICommandResult<Notificacao> Handler(AdicionarTipoPagamentoCommand command);
        ICommandResult<Notificacao> Handler(AtualizarTipoPagamentoCommand command);
        ICommandResult<Notificacao> Handler(ApagarTipoPagamentoCommand command);
    }
}