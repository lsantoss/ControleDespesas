using ControleDespesas.Domain.TiposPagamentos.Commands.Input;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Domain.TiposPagamentos.Interfaces.Handlers
{
    public interface ITipoPagamentoHandler
    {
        ICommandResult<Notificacao> Handler(AdicionarTipoPagamentoCommand command);
        ICommandResult<Notificacao> Handler(long id, AtualizarTipoPagamentoCommand command);
        ICommandResult<Notificacao> Handler(long id);
    }
}