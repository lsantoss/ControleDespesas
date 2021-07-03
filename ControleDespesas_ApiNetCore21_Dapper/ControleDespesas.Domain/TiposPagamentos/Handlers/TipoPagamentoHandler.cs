using ControleDespesas.Domain.TiposPagamentos.Commands.Input;
using ControleDespesas.Domain.TiposPagamentos.Helpers;
using ControleDespesas.Domain.TiposPagamentos.Interfaces.Handlers;
using ControleDespesas.Domain.TiposPagamentos.Interfaces.Repositories;
using ControleDespesas.Infra.Commands;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Http;

namespace ControleDespesas.Domain.TiposPagamentos.Handlers
{
    public class TipoPagamentoHandler : Notificadora, ITipoPagamentoHandler
    {
        private readonly ITipoPagamentoRepository _repository;

        public TipoPagamentoHandler(ITipoPagamentoRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult<Notificacao> Handler(AdicionarTipoPagamentoCommand command)
        {
            if (command == null)
                return new CommandResult(StatusCodes.Status400BadRequest, "Parâmentros inválidos", "Parâmetros de entrada", "Parâmetros de entrada estão nulos");

            if (!command.ValidarCommand())
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Parâmentros inválidos", command.Notificacoes);

            var tipoPagamento = TipoPagamentoHelper.GerarEntidade(command);

            if (tipoPagamento.Invalido)
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", tipoPagamento.Notificacoes);

            var id = _repository.Salvar(tipoPagamento);
            tipoPagamento.DefinirId(id);

            var dadosRetorno = TipoPagamentoHelper.GerarDadosRetorno(tipoPagamento);

            return new CommandResult(StatusCodes.Status201Created, "Tipo Pagamento gravado com sucesso!", dadosRetorno);
        }

        public ICommandResult<Notificacao> Handler(long id, AtualizarTipoPagamentoCommand command)
        {
            if (command == null)
                return new CommandResult(StatusCodes.Status400BadRequest, "Parâmentros inválidos", "Parâmetros de entrada", "Parâmetros de entrada estão nulos");

            command.Id = id;

            if (!command.ValidarCommand())
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Parâmentros inválidos", command.Notificacoes);

            var tipoPagamento = TipoPagamentoHelper.GerarEntidade(command);

            if (tipoPagamento.Invalido)
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", tipoPagamento.Notificacoes);

            if (!_repository.CheckId(tipoPagamento.Id))
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", "Id", "Id inválido. Este id não está cadastrado!");

            _repository.Atualizar(tipoPagamento);

            var dadosRetorno = TipoPagamentoHelper.GerarDadosRetorno(tipoPagamento);

            return new CommandResult(StatusCodes.Status200OK, "Tipo Pagamento atualizado com sucesso!", dadosRetorno);
        }

        public ICommandResult<Notificacao> Handler(long id)
        {
            if (!_repository.CheckId(id))
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", "Id", "Id inválido. Este id não está cadastrado!");

            _repository.Deletar(id);

            var dadosRetorno = TipoPagamentoHelper.GerarDadosRetornoDelete(id);

            return new CommandResult(StatusCodes.Status200OK, "Tipo Pagamento excluído com sucesso!", dadosRetorno);
        }
    }
}