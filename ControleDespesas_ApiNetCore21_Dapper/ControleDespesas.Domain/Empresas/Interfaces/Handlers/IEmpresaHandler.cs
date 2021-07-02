using ControleDespesas.Domain.Empresas.Commands.Input;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Domain.Empresas.Interfaces.Handlers
{
    public interface IEmpresaHandler
    {
        ICommandResult<Notificacao> Handler(AdicionarEmpresaCommand command);
        ICommandResult<Notificacao> Handler(long id, AtualizarEmpresaCommand command);
        ICommandResult<Notificacao> Handler(long id);
    }
}